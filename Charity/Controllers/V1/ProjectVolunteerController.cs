using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.ProjectVolunteers.Commands.AddVolunteerToProject;
using Charity.Application.Features.V1.ProjectVolunteers.Commands.RemoveVolunteerFromProject;
using Charity.Application.Features.V1.ProjectVolunteers.Queries.GetAllVolunteersInProject;
using Charity.Models.ProjectVolunteer;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProjectVolunteerController : BaseApiController
    {
        [HttpPost("AddVolunteerToProject")]
        public async Task<IActionResult> AddVolunteerToProject([FromBody] ProjectVolunteerModel projectVolunteer)
        {
            return NewResult(await Mediator.Send(new AddVolunteerToProjectCommand(projectVolunteer)));
        }

        [HttpPost("RemoveVolunteerFromProject")]
        public async Task<IActionResult> RemoveVolunteerFromProject([FromBody] ProjectVolunteerModel projectVolunteer)
        {
            return NewResult(await Mediator.Send(new RemoveVolunteerFromProjectCommand(projectVolunteer)));
        }

        [HttpGet("GetAllVolunteersInProject")]
        public async Task<IActionResult> GetAllVolunteersInProject([FromQuery] string projectId)
        {
            return NewResult(await Mediator.Send(new GetAllVolunteersInProjectQuery(projectId)));
        }
    }
}
