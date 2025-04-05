using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Role.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<RoleModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetRoleByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<RoleModel>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityRoles.GetByAsync(u => u.Id == request.Id,
                    cancellationToken: cancellationToken);
                if (user == null)
                    return ResponseHandler.NotFound<RoleModel>(message: "Not Found Role.");

                var result = _mapper.Map<RoleModel>(user);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Role data.");
                return ResponseHandler.BadRequest<RoleModel>(errors: ex.Message);
            }
        }
    }
}
