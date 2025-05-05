using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authorization.Commands.RemoveUserFromRole
{
    public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveUserFromRoleCommandHandler> _logger;

        public RemoveUserFromRoleCommandHandler(IUnitOfWork unitOfWork,
            ILogger<RemoveUserFromRoleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByIdAsync(request.UserRoleModel.UserId);
                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User not found.");

                var role = await _unitOfWork.CharityRoles.RoleManager.FindByIdAsync(request.UserRoleModel.RoleId);
                if (role is null)
                    return ResponseHandler.NotFound<string>(message: "Role not found.");

                IdentityResult result = await _unitOfWork.CharityUsers.UserManager.RemoveFromRoleAsync(user, role.Name!);
                if (result.Succeeded)
                    return ResponseHandler.Success<string>(message: "User removed to role Successfully.");

                return ResponseHandler.BadRequest<string>(errors: "Failed removing user to role.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during remove user to role.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
