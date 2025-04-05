using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateRoleCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _unitOfWork.CharityRoles.RoleManager.FindByIdAsync(request.Id);
                if (role is null)
                    return ResponseHandler.NotFound<string>(message: "Role Not Found.");

                request.RoleModel.ModifiedDate = DateTime.UtcNow;
                _mapper.Map(request.RoleModel, role);
                IdentityResult result = await _unitOfWork.CharityRoles.RoleManager.UpdateAsync(role);

                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(message: "Check your input data.");
                return ResponseHandler.Success<string>(message: "The role has been updated successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during Update Role.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
