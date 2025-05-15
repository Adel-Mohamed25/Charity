using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;

namespace Charity.Application.Features.V1.Role.Commands.UpdateRole
{
    public record UpdateRoleCommand(UpdateRoleModel RoleModel) : IRequest<Response<string>>;
}
