using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRoleCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork,
            ILogger<CreateRoleCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = _mapper.Map<CharityRole>(request.RoleModel);
                var result = await _unitOfWork.CharityRoles.RoleManager.CreateAsync(role);
                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(errors: "Error occured during input data for create role.");
                return ResponseHandler.Success<string>(message: "The role has been created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create role.");
                return ResponseHandler.BadRequest<string>(message: ex.Message);
            }
        }
    }
}
