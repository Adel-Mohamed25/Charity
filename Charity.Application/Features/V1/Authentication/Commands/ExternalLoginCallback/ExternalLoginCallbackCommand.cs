using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.ExternalLoginCallback
{
    public record ExternalLoginCallbackCommand() : IRequest<Response<AuthModel>>;
}
