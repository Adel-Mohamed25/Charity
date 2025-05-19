using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.AssistanceRequests.Commands.CreateAssistanceRequest;
using Charity.Application.Features.V1.AssistanceRequests.Commands.DeleteAssistanceRequest;
using Charity.Application.Features.V1.AssistanceRequests.Commands.UpdateAssistanceRequest;
using Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequests;
using Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequestsById;
using Charity.Application.Features.V1.AssistanceRequests.Queries.GetAssistanceRequestById;
using Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedAssistanceRequests;
using Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedByRequestStatus;
using Charity.Domain.Enum;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AssistanceRequestController : BaseApiController
    {
        [HttpPost("CreateAssistanceRequest")]
        public async Task<IActionResult> CreateAssistanceRequest([FromBody] CreateAssistanceRequestModel createAssistance)
        {
            return NewResult(await Mediator.Send(new CreateAssistanceRequestCommand(createAssistance)));
        }

        [HttpPut("UpdateAssistanceRequest")]
        public async Task<IActionResult> UpdateAssistanceRequest([FromBody] UpdateAssistanceRequestModel updateAssistanceRequest)
        {
            return NewResult(await Mediator.Send(new UpdateAssistanceRequestCommand(updateAssistanceRequest)));
        }

        [HttpDelete("DeleteAssistanceRequest")]
        public async Task<IActionResult> DeleteAssistanceRequest([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteAssistanceRequestCommand(id)));
        }

        [HttpGet("GetAllAssistanceRequests")]
        public async Task<IActionResult> GetAllAssistanceRequests()
        {
            return NewResult(await Mediator.Send(new GetAllAssistanceRequestsQuery()));
        }

        [HttpGet("GetAllAssistanceRequestsById")]
        public async Task<IActionResult> GetAllAssistanceRequestsById(string id)
        {
            return NewResult(await Mediator.Send(new GetAllAssistanceRequestsByIdQuery(id)));
        }

        [HttpGet("GetPaginatedAssistanceRequests")]
        public async Task<IActionResult> GetPaginatedAssistanceRequests([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedAssistanceRequestsQuery(pagination)));
        }

        [HttpGet("GetPaginatedByRequestStatus")]
        public async Task<IActionResult> GetPaginatedByRequestStatus([FromQuery] RequestStatus requestStatus, [FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedByRequestStatusQuery(requestStatus, pagination)));
        }

        [HttpGet("GetAssistanceRequestById")]
        public async Task<IActionResult> GetAssistanceRequestById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetAssistanceRequestByIdQuery(id)));
        }

    }
}
