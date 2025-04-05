using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.AidDistributions.Commands.CreateAidDistribution;
using Charity.Application.Features.V1.AidDistributions.Commands.DeleteAidDistribution;
using Charity.Application.Features.V1.AidDistributions.Commands.UpdateAidDistribution;
using Charity.Application.Features.V1.AidDistributions.Queries.GetAidDistributionById;
using Charity.Application.Features.V1.AidDistributions.Queries.GetAllAidDistributions;
using Charity.Application.Features.V1.AidDistributions.Queries.GetPaginatedAidDistributions;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AidDistributionController : BaseApiController
    {
        [HttpPost("CreateAidDistribution")]
        public async Task<IActionResult> CreateAidDistribution([FromBody] CreateAidDistributionModel aidDistributionModel)
        {
            return NewResult(await Mediator.Send(new CreateAidDistributionCommand(aidDistributionModel)));
        }

        [HttpPut("UpdateAidDistribution")]
        public async Task<IActionResult> UpdateAidDistribution([FromBody] UpdateAidDistributionModel aidDistributionModel)
        {
            return NewResult(await Mediator.Send(new UpdateAidDistributionCommand(aidDistributionModel)));
        }

        [HttpDelete("DeleteAidDistribution")]
        public async Task<IActionResult> DeleteAidDistribution([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteAidDistributionCommand(id)));
        }

        [HttpGet("GetAllAidDistributions")]
        public async Task<IActionResult> GetAllAidDistributions()
        {
            return NewResult(await Mediator.Send(new GetAllAidDistributionsQuery()));
        }

        [HttpGet("GetPaginatedAidDistributions")]
        public async Task<IActionResult> GetPaginatedAidDistributions([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedAidDistributionsQuery(pagination)));
        }

        [HttpGet("GetAidDistributionById")]
        public async Task<IActionResult> GetAidDistributionById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetAidDistributionByIdQuery(id)));
        }
    }
}
