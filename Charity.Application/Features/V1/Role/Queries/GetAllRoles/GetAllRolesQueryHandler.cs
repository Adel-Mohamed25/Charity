using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Role.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<IEnumerable<RoleModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllRolesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllRolesQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<RoleModel>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _unitOfWork.CharityRoles.GetAllAsync(orderBy: u => u.Name!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!roles.Any())
                    return ResponseHandler.NotFound<IEnumerable<RoleModel>>(message: "Not Found Roles.");

                var result = await roles.ProjectTo<RoleModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles data.");
                return ResponseHandler.BadRequest<IEnumerable<RoleModel>>(errors: ex.Message);
            }
        }
    }
}
