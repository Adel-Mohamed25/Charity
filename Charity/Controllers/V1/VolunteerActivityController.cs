using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.VolunteerActivities.Commands.CreateVolunteerActivity;
using Charity.Application.Features.V1.VolunteerActivities.Commands.DeleteVolunteerActivity;
using Charity.Application.Features.V1.VolunteerActivities.Commands.UpdateVolunteerActivity;
using Charity.Application.Features.V1.VolunteerActivities.Queries.GetAllVolunteerActivities;
using Charity.Application.Features.V1.VolunteerActivities.Queries.GetPaginatedVolunteerActivities;
using Charity.Application.Features.V1.VolunteerActivities.Queries.GetVolunteerActivityById;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VolunteerActivityController : BaseApiController
    {
        [HttpPost("CreateVolunteerActivity")]
        public async Task<IActionResult> CreateVolunteerActivity([FromBody] CreateVolunteerActivityModel volunteerActivityModel)
        {
            return NewResult(await Mediator.Send(new CreateVolunteerActivityCommand(volunteerActivityModel)));
        }

        [HttpPut("UpdateVolunteerActivity")]
        public async Task<IActionResult> UpdateVolunteerActivity([FromBody] UpdateVolunteerActivityModel volunteerActivityModel)
        {
            return NewResult(await Mediator.Send(new UpdateVolunteerActivityCommand(volunteerActivityModel)));
        }

        [HttpDelete("DeleteVolunteerActivity")]
        public async Task<IActionResult> DeleteVolunteerActivity([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteVolunteerActivityCommand(id)));
        }

        [HttpGet("GetAllVolunteerActivities")]
        public async Task<IActionResult> GetAllVolunteerActivities()
        {
            return NewResult(await Mediator.Send(new GetAllVolunteerActivitiesQuery()));
        }

        [HttpGet("GetPaginatedVolunteerActivities")]
        public async Task<IActionResult> GetPaginatedVolunteerActivities([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedVolunteerActivitiesQuery(pagination)));
        }

        [HttpGet("GetVolunteerActivityById")]
        public async Task<IActionResult> GetVolunteerActivityById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetVolunteerActivityByIdQuery(id)));
        }
    }
}
