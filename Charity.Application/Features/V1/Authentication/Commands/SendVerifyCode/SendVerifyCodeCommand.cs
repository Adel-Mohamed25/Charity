using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.GenerateVerifyCode
{
    public record SendVerifyCodeCommand(UserEmailModel UserEmail) : IRequest<Response<string>>;
}
