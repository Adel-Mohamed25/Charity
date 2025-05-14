using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        [HttpPost("CreatePaymentIntent")]
        public async Task<IActionResult> CreatePaymentIntent(decimal amount, string donorId, string? projectId)
        {
            return NewResult(await Mediator.Send(new CreatePaymentIntentCommand(amount, donorId, projectId)));
        }

        [HttpPost("Webhook")]
        public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken)
        {
            await UnitOfService.PaymentServices.HandleStripeWebhookAsync(Request, cancellationToken);
            return Ok();
        }
    }
}
