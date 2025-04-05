using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.Role.Commands.CreateRole;
using Charity.Application.Features.V1.Role.Commands.DeleteRole;
using Charity.Application.Features.V1.Role.Commands.UpdateRole;
using Charity.Application.Features.V1.Role.Queries.GetAllRoles;
using Charity.Application.Features.V1.Role.Queries.GetRoleById;
using Charity.Models.Role;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    //[Authorize(Roles = "Admin, SuperAdmin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel roleModel)
        {
            return NewResult(await Mediator.Send(new CreateRoleCommand(roleModel)));
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromQuery] string Id, [FromBody] RoleModel roleModel)
        {
            return NewResult(await Mediator.Send(new UpdateRoleCommand(Id, roleModel)));
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] string Id)
        {
            return NewResult(await Mediator.Send(new DeleteRoleCommand(Id)));
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return NewResult(await Mediator.Send(new GetAllRolesQuery()));
        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] string Id)
        {
            return NewResult(await Mediator.Send(new GetRoleByIdQuery(Id)));
        }

    }
}
