using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IUnitOfService _unitOfService;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteUserCommandHandler> logger,
            IUnitOfService unitOfService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _unitOfService = unitOfService;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByIdAsync(request.Id);
                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User Not Found.");

                if (user.ImageUrl is not null)
                    await _unitOfService.FileServices.DeleteImageAsync("ProjectImages", user.ImageUrl);

                IdentityResult result = await _unitOfWork.CharityUsers.UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return ResponseHandler.BadRequest<string>(errors: "An error occurred while deleting the user.");
                return ResponseHandler.Success<string>(message: "The user has been deleted successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete user.");
                return ResponseHandler.BadRequest<string>(message: ex.Message);
            }
        }
    }
}
