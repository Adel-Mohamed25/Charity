using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfService _unitOfService;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateUserCommandHandler> logger,
            IMapper mapper,
            IUnitOfService unitOfService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _unitOfService = unitOfService;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByIdAsync(request.Id);
                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User Not Found.");

                if (request.UpdateUser.Image is null && request.UpdateUser.ImageUrl == null && user.ImageUrl != null)
                {
                    await _unitOfService.FileServices.DeleteImageAsync("UsersImages", user.ImageUrl);
                    request.UpdateUser.ImageUrl = null;
                }
                else if (request.UpdateUser.Image is not null)
                {
                    if (!string.IsNullOrEmpty(user.ImageUrl))
                    {
                        await _unitOfService.FileServices.DeleteImageAsync("UsersImages", user.ImageUrl);
                    }
                    var imageUrl = await _unitOfService.FileServices.UploadImageAsync("UsersImages", request.UpdateUser.Image);
                    request.UpdateUser.ImageUrl = imageUrl;
                }
                _mapper.Map(request.UpdateUser, user);


                IdentityResult result = await _unitOfWork.CharityUsers.UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(message: "Check your input data.");
                return ResponseHandler.Success<string>(message: "The user has been updated successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during Update User.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
