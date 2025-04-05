using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.UserVolunteerActivities.Commands.AddVolunteerToActivity;
using Charity.Application.Features.V1.UserVolunteerActivities.Commands.RemoveVolunteerFromActivity;
using Charity.Application.Features.V1.UserVolunteerActivities.Queries.GetAllVolunteersInActivity;
using Charity.Models.UserVolunteerActivity;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserVolunteerActivityController : BaseApiController
    {
        [HttpPost("AddVolunteerToActivity")]
        public async Task<IActionResult> AddVolunteerToProject([FromBody] UserVolunteerActivityModel userVolunteerActivity)
        {
            return NewResult(await Mediator.Send(new AddVolunteerToActivityCommand(userVolunteerActivity)));
        }

        [HttpPost("RemoveVolunteerFromActivity")]
        public async Task<IActionResult> RemoveVolunteerFromProject([FromBody] UserVolunteerActivityModel userVolunteerActivity)
        {
            return NewResult(await Mediator.Send(new RemoveVolunteerFromActivityCommand(userVolunteerActivity)));
        }

        [HttpGet("GetAllVolunteersInActivity")]
        public async Task<IActionResult> GetAllVolunteersInProject([FromQuery] string volunteerActivityId)
        {
            return NewResult(await Mediator.Send(new GetAllVolunteersInActivityQuery(volunteerActivityId)));
        }
    }
}
