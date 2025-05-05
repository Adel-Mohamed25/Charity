using Charity.Models.Authorization;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authorization.Commands.AddUserToRole
{
    public record AddUserToRoleCommand(UserRoleModel UserRoleModel) : IRequest<Response<string>>;
}
