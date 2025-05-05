using Charity.Models.Authorization;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authorization.Commands.RemoveUserFromRole
{
    public record RemoveUserFromRoleCommand(UserRoleModel UserRoleModel) : IRequest<Response<string>>;
}
