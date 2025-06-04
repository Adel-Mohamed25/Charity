using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.Webhook
{
    public record WebhookCommand(HttpRequest Request) : IRequest<IActionResult>;
}
