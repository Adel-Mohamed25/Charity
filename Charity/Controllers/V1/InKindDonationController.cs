using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.InKindDonations.Commands.CreateInKindDonation;
using Charity.Application.Features.V1.InKindDonations.Commands.DeleteInKindDonation;
using Charity.Application.Features.V1.InKindDonations.Commands.UpdateInKindDonation;
using Charity.Application.Features.V1.InKindDonations.Queries.GetAllinKindDonations;
using Charity.Application.Features.V1.InKindDonations.Queries.GetinKindDonationById;
using Charity.Application.Features.V1.InKindDonations.Queries.GetPaginatedinKindDonations;
using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InKindDonationController : BaseApiController
    {
        [HttpPost("CreateInKindDonation")]
        public async Task<IActionResult> CreateInKindDonation([FromForm] CreateInKindDonationModel inKindDonationModel)
        {
            return NewResult(await Mediator.Send(new CreateInKindDonationCommand(inKindDonationModel)));
        }

        [HttpPut("UpdateInKindDonation")]
        public async Task<IActionResult> UpdateInKindDonation([FromForm] UpdateInKindDonationModel inKindDonationModel)
        {
            return NewResult(await Mediator.Send(new UpdateInKindDonationCommand(inKindDonationModel)));
        }

        [HttpDelete("DeleteInKindDonation")]
        public async Task<IActionResult> DeleteInKindDonation([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteInKindDonationCommand(id)));
        }

        [HttpGet("GetAllInKindDonations")]
        public async Task<IActionResult> GetAllInKindDonations()
        {
            return NewResult(await Mediator.Send(new GetAllinKindDonationsQuery()));
        }

        [HttpGet("GetPaginatedinKindDonations")]
        public async Task<IActionResult> GetPaginatedinKindDonations([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedinKindDonationsQuery(pagination)));
        }

        [HttpGet("GetinKindDonationById")]
        public async Task<IActionResult> GetinKindDonationById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetinKindDonationByIdQuery(id)));
        }


    }
}
