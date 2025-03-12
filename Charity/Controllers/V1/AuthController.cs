using Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail;
using Charity.Application.Features.V1.Authentication.Commands.LockAccount;
using Charity.Application.Features.V1.Authentication.Commands.Login;
using Charity.Application.Features.V1.Authentication.Commands.RefreshToken;
using Charity.Application.Features.V1.Authentication.Commands.Register;
using Charity.Application.Features.V1.Authentication.Commands.ResetPassword;
using Charity.Application.Features.V1.Authentication.Commands.SendVerifyCode;
using Charity.Application.Features.V1.Authentication.Commands.UnLockAccount;
using Charity.Application.Features.V1.Authentication.Commands.VerifyCode;
using Charity.Models.Authentication;
using Charity.Models.Email;
using Charity.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Charity.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            return NewResult(await Mediator.Send(new LoginCommand(loginModel)));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel createUser)
        {
            return NewResult(await Mediator.Send(new RegisterCommand(createUser)));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshJwtRequestModel refreshJwt)
        {
            return NewResult(await Mediator.Send(new RefreshTokenCommand(refreshJwt)));
        }

        [HttpPost("SendVerifyCode")]
        public async Task<IActionResult> SendVerifyCode([FromBody] UserEmailModel userEmail)
        {
            return NewResult(await Mediator.Send(new SendVerifyCodeCommand(userEmail)));
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestModel verifyCodeRequest)
        {
            return NewResult(await Mediator.Send(new VerifyCodeCommand(verifyCodeRequest)));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword)
        {
            return NewResult(await Mediator.Send(new ResetPasswordCommand(resetPassword)));
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmationRequest emailConfirmation)
        {
            return NewResult(await Mediator.Send(new ConfirmEmailCommand(emailConfirmation)));
        }

        [HttpPost("LockAccount")]
        public async Task<IActionResult> LockAccount([FromBody] UserEmailModel userEmail)
        {
            return NewResult(await Mediator.Send(new LockAccountCommand(userEmail)));
        }

        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnLockAccount(UserEmailModel userEmail)
        {
            return NewResult(await Mediator.Send(new UnLockAccountCommand(userEmail)));
        }

    }
}
