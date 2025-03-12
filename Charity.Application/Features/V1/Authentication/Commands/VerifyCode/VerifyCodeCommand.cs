using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.VerifyCode
{
    public record VerifyCodeCommand(VerifyCodeRequestModel VerifyCode) : IRequest<Response<string>>;
}
