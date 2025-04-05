using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;
using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Charity.Application.Features.V1.Authentication.Commands.ExternalLoginCallback
{
    public class ExternalLoginCallbackCommandHandler : IRequestHandler<ExternalLoginCallbackCommand, Response<AuthModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<ExternalLoginCallbackCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ExternalLoginCallbackCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<ExternalLoginCallbackCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<AuthModel>> Handle(ExternalLoginCallbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var externalLoginInfo = await _unitOfWork.CharityUsers.SignInManager.GetExternalLoginInfoAsync();
                if (externalLoginInfo == null)
                {
                    _logger.LogError("Failed to retrieve external login info.");
                    return ResponseHandler.BadRequest<AuthModel>(message: "Failed to retrieve external login info.");
                }

                var signInResult = await _unitOfWork.CharityUsers.SignInManager.ExternalLoginSignInAsync(
                    externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey,
                    isPersistent: false
                );

                if (signInResult.Succeeded)
                {
                    var user = await _unitOfWork.CharityUsers.UserManager.FindByLoginAsync(
                        externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey
                    );
                    if (user == null)
                    {
                        return ResponseHandler.BadRequest<AuthModel>(message: "User not found.");
                    }

                    var authModel = await _unitOfService.AuthServices.GetTokenAsync(user);
                    return ResponseHandler.Success(authModel, "Login successful.");
                }
                else
                {
                    var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
                    if (email == null)
                    {
                        return ResponseHandler.BadRequest<AuthModel>(message: "Email is required.");
                    }

                    var findUser = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(email);
                    if (findUser == null)
                    {
                        var newUser = new CreateUserModel
                        {
                            FirstName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "",
                            LastName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Surname) ?? "",
                            Email = email,
                            UserType = UserRole.Beneficiary,
                            PhoneNumber = "01143254939",
                        };

                        var convertUser = _mapper.Map<CharityUser>(newUser);
                        convertUser.UserName = email;
                        convertUser.EmailConfirmed = true;
                        var createUserResult = await _unitOfWork.CharityUsers.UserManager.CreateAsync(convertUser);
                        if (!createUserResult.Succeeded)
                        {
                            return ResponseHandler.BadRequest<AuthModel>(message: "Failed to create user.");
                        }

                        await _unitOfWork.CharityUsers.UserManager.AddLoginAsync(convertUser, externalLoginInfo);
                        await _unitOfWork.CharityUsers.SignInManager.SignInAsync(convertUser, isPersistent: false);

                        await _unitOfWork.CharityUsers.UserManager.AddToRoleAsync(convertUser, UserRole.Beneficiary.ToString());

                        var authModel = await _unitOfService.AuthServices.GetTokenAsync(convertUser);
                        return ResponseHandler.Success(authModel, "New user registered and logged in.");
                    }

                    await _unitOfWork.CharityUsers.UserManager.AddLoginAsync(findUser, externalLoginInfo);
                    await _unitOfWork.CharityUsers.SignInManager.SignInAsync(findUser, isPersistent: false);

                    var authModelExisting = await _unitOfService.AuthServices.GetTokenAsync(findUser);
                    return ResponseHandler.Success(authModelExisting, "Login successful.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during external login callback.");
                return ResponseHandler.BadRequest<AuthModel>(message: $"An error occurred while processing your request. Exception: {ex.Message} .");
            }
        }
    }
}
