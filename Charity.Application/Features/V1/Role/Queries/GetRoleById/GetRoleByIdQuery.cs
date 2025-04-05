using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;

namespace Charity.Application.Features.V1.Role.Queries.GetRoleById
{
    public record GetRoleByIdQuery(string Id) : IRequest<Response<RoleModel>>;
}
