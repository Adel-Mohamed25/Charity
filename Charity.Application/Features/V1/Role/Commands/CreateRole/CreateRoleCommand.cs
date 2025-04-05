using Charity.Models.ResponseModels;
using Charity.Models.Role;
using MediatR;

namespace Charity.Application.Features.V1.Role.Commands.CreateRole
{
    public record CreateRoleCommand(CreateRoleModel RoleModel) : IRequest<Response<string>>;
}
