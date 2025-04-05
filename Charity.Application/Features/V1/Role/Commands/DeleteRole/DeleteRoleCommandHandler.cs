using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteRoleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _unitOfWork.CharityRoles.RoleManager.FindByIdAsync(request.Id);
                if (role is null)
                    return ResponseHandler.NotFound<string>(message: "role Not Found.");

                IdentityResult result = await _unitOfWork.CharityRoles.RoleManager.DeleteAsync(role);
                if (!result.Succeeded)
                    return ResponseHandler.BadRequest<string>(errors: "An error occurred while deleting the role.");
                return ResponseHandler.Success<string>(message: "The role has been deleted successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete role.");
                return ResponseHandler.BadRequest<string>(message: ex.Message);
            }
        }
    }
}
