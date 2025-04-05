using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Email.Commands.SendEmail
{
    public record SendEmailCommand(SendEmailRequest SendEmail) : IRequest<Response<SendEmailResponse>>;
}
