using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace Charity.Application.Features.V1.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<AuthModel>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<LoginCommandHandler> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<AuthModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region Get IP address from HttpContext
                var remoteIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

                if (remoteIpAddress != null && remoteIpAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    _logger.LogInformation($"Client IPv4 Address: {remoteIpAddress}");
                }
                else
                {
                    _logger.LogWarning("IPv4 Address Not Found.");
                }
                #endregion

                #region Get host details
                var hostName = Dns.GetHostName();
                var hostEntry = await Dns.GetHostEntryAsync(hostName);
                _logger.LogInformation($"Host Name: {hostEntry.HostName}");
                #endregion

                var findUser = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(request.loginModel.Email);
                if (findUser == null)
                    return ResponseHandler.NotFound<AuthModel>(errors: "User not found");

                if (await _unitOfWork.CharityUsers.UserManager.IsLockedOutAsync(findUser))
                {
                    _logger.LogWarning($"User {findUser.Email} attempted to login but is locked out.");
                    return ResponseHandler.BadRequest<AuthModel>(errors: "Your account is locked. Please contact support if you need assistance.");
                }

                bool isPasswordCorrect = await _unitOfWork.CharityUsers.UserManager.CheckPasswordAsync(findUser, request.loginModel.Password);

                if (!isPasswordCorrect)
                {
                    await _unitOfWork.CharityUsers.UserManager.AccessFailedAsync(findUser);

                    if (await _unitOfWork.CharityUsers.UserManager.IsLockedOutAsync(findUser))
                    {
                        _logger.LogWarning($"User {findUser.Email} has been locked out due to multiple failed attempts.");
                        return ResponseHandler.Conflict<AuthModel>(errors: "Your account has been locked due to multiple failed login attempts. Try again in 5 minutes or contact support if you need assistance.");
                    }

                    return ResponseHandler.BadRequest<AuthModel>(errors: "Incorrect email or password");
                }


                await _unitOfWork.CharityUsers.UserManager.ResetAccessFailedCountAsync(findUser);

                var user = await _unitOfWork.CharityUsers.GetByAsync(
                   mandatoryFilter: u => u.Id == findUser.Id,
                   cancellationToken: cancellationToken,
                   includes: $"{nameof(findUser.JwtTokens)}"
               );

                var authModel = await _unitOfService.AuthServices.GetTokenAsync(user);
                return ResponseHandler.Success(authModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user login.");
                return ResponseHandler.BadRequest<AuthModel>(errors: ex.Message);
            }
        }
    }
}
