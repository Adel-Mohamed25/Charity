using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshJwtRequestModel RefreshJwtModel) : IRequest<Response<AuthModel>>;
}
