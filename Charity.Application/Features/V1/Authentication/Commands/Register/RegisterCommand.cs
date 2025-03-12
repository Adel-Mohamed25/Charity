using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.Register
{
    public record RegisterCommand(CreateUserModel CreateUser) : IRequest<Response<AuthModel>>;
}
