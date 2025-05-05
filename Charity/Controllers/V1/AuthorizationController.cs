using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.Authorization.Commands.AddUserToRole;
using Charity.Application.Features.V1.Authorization.Commands.RemoveUserFromRole;
using Charity.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseApiController
    {
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole([FromBody] UserRoleModel userRoleModel)
        {
            return NewResult(await Mediator.Send(new AddUserToRoleCommand(userRoleModel)));
        }

        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole([FromBody] UserRoleModel userRoleModel)
        {
            return NewResult(await Mediator.Send(new RemoveUserFromRoleCommand(userRoleModel)));
        }
    }
}
