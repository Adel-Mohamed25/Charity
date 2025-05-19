using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.VolunteerApplications.Commands.CreateVolunteerApplication;
using Charity.Application.Features.V1.VolunteerApplications.Commands.DeleteVolunteerApplication;
using Charity.Application.Features.V1.VolunteerApplications.Commands.UpdateVolunteerApplication;
using Charity.Application.Features.V1.VolunteerApplications.Queries.GetAllVolunteerApplications;
using Charity.Application.Features.V1.VolunteerApplications.Queries.GetPaginatedByRequestStatus;
using Charity.Application.Features.V1.VolunteerApplications.Queries.GetPaginatedVolunteerApplications;
using Charity.Application.Features.V1.VolunteerApplications.Queries.GetVolunteerApplicationById;
using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VolunteerApplicationController : BaseApiController
    {
        [HttpPost("CreateVolunteerApplication")]
        public async Task<IActionResult> CreateVolunteerApplication([FromBody] CreateVolunteerApplicationModel volunteerApplicationModel)
        {
            return NewResult(await Mediator.Send(new CreateVolunteerApplicationCommand(volunteerApplicationModel)));
        }

        [HttpPut("UpdateVolunteerApplication")]
        public async Task<IActionResult> UpdateVolunteerApplication([FromBody] UpdateVolunteerApplicationModel volunteerApplicationModel)
        {
            return NewResult(await Mediator.Send(new UpdateVolunteerApplicationCommand(volunteerApplicationModel)));
        }

        [HttpDelete("DeleteVolunteerApplication")]
        public async Task<IActionResult> DeleteVolunteerApplication([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteVolunteerApplicationCommand(id)));
        }

        [HttpGet("GetAllVolunteerApplications")]
        public async Task<IActionResult> GetAllVolunteerApplications()
        {
            return NewResult(await Mediator.Send(new GetAllVolunteerApplicationsQuery()));
        }

        [HttpGet("GetPaginatedVolunteerApplications")]
        public async Task<IActionResult> GetPaginatedVolunteerApplications([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedVolunteerApplicationsQuery(pagination)));
        }

        [HttpGet("GetPaginatedByRequestStatus")]
        public async Task<IActionResult> GetPaginatedByRequestStatus([FromQuery] RequestStatus requestStatus, [FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedByRequestStatusQuery(requestStatus, pagination)));
        }

        [HttpGet("GetVolunteerApplicationById")]
        public async Task<IActionResult> GetVolunteerApplicationById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetVolunteerApplicationByIdQuery(id)));
        }
    }
}
