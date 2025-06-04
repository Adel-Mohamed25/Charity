using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent;
using Charity.Application.Features.V1.MonetaryDonations.Commands.Webhook;
using Charity.Application.Features.V1.MonetaryDonations.Queries.GetMonetaryDonationById;
using Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonations;
using Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonationsByDonorId;
using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        [HttpPost("CreatePaymentIntent")]
        public async Task<IActionResult> CreatePaymentIntent([FromQuery] CreatePaymentModel paymentModel)
        {
            return NewResult(await Mediator.Send(new CreatePaymentIntentCommand(paymentModel)));
        }

        [HttpPost("Webhook")]
        public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new WebhookCommand(Request));
        }

        [HttpGet("GetMonetaryDonationById")]
        public async Task<IActionResult> GetMonetaryDonationById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetMonetaryDonationByIdQuery(id)));
        }

        [HttpGet("GetPaginatedMonetaryDonations")]
        public async Task<IActionResult> GetPaginatedMonetaryDonations([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedMonetaryDonationsQuery(pagination)));
        }

        [HttpGet("GetPaginatedMonetaryDonationsByDonorId")]
        public async Task<IActionResult> GetPaginatedMonetaryDonationsByDonorId([FromQuery] string donorId,
            [FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedMonetaryDonationsByDonorIdQuery(donorId, pagination)));
        }
    }
}
