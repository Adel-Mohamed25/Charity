using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(EmailConfirmationRequest EmailConfirmation) : IRequest<Response<EmailConfirmationResponse>>;
}
