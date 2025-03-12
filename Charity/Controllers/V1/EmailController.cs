using Charity.Application.Features.V1.Email.Commands.SendEmail;
using Charity.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace Charity.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmailController : BaseApiController
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailModel sendEmail)
        {
            return NewResult(await Mediator.Send(new SendEmailCommand(sendEmail)));
        }

    }
}
