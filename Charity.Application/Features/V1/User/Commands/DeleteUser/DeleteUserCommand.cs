using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.User.Commands.DeleteUser
{
    public record DeleteUserCommand(string Id) : IRequest<Response<string>>;
}
