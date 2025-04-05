using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.User.Commands.DeleteUser;
using Charity.Application.Features.V1.User.Commands.UpdateUser;
using Charity.Application.Features.V1.User.Queries.GetAllUsers;
using Charity.Application.Features.V1.User.Queries.GetPaginatedUsers;
using Charity.Application.Features.V1.User.Queries.GetUserById;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string Id, [FromForm] UpdateUserModel updateUser)
        {
            return NewResult(await Mediator.Send(new UpdateUserCommand(Id, updateUser)));
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string Id)
        {
            return NewResult(await Mediator.Send(new DeleteUserCommand(Id)));
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return NewResult(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("GetPaginatedUsers")]
        public async Task<IActionResult> GetPaginatedUsers([FromQuery] PaginationModel pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedUsersQuery(pagination)));
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string Id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(Id)));
        }
    }
}
