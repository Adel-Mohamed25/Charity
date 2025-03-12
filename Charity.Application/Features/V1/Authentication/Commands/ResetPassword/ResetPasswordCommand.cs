using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordModel ResetPassword) : IRequest<Response<string>>;
}
