using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.Project.Commands.CreateProject;
using Charity.Application.Features.V1.Project.Commands.DeleteProject;
using Charity.Application.Features.V1.Project.Commands.UpdateProject;
using Charity.Application.Features.V1.Project.Queries.GetAllProjects;
using Charity.Application.Features.V1.Project.Queries.GetPaginatedProjects;
using Charity.Application.Features.V1.Project.Queries.GetProjectById;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProjectController : BaseApiController
    {
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectModel projectModel)
        {
            return NewResult(await Mediator.Send(new CreateProjectCommand(projectModel)));
        }

        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject([FromForm] UpdateProjectModel projectModel)
        {
            return NewResult(await Mediator.Send(new UpdateProjectCommand(projectModel)));
        }

        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteProjectCommand(id)));
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            return NewResult(await Mediator.Send(new GetAllProjectsQuery()));
        }

        [HttpGet("GetPaginatedProjects")]
        public async Task<IActionResult> GetPaginatedProjects([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedProjectsQuery(pagination)));
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetProjectByIdQuery(id)));
        }
    }
}
