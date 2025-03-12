using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.Login
{
    public record LoginCommand(LoginModel loginModel) : IRequest<Response<AuthModel>>;
}
