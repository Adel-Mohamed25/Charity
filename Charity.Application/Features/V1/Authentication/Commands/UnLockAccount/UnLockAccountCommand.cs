using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.UnLockAccount
{
    public record UnLockAccountCommand(UserEmailModel UserEmail) : IRequest<Response<string>>;

}
