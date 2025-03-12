using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.LockAccount
{
    public record LockAccountCommand(UserEmailModel UserEmail) : IRequest<Response<string>>;
}
