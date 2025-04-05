using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Role.Commands.DeleteRole
{
    public record DeleteRoleCommand(string Id) : IRequest<Response<string>>;
}
