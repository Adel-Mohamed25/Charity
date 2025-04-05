using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;

namespace Charity.Application.Features.V1.Role.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<Response<IEnumerable<RoleModel>>>;
}
