using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail;
using Charity.Application.Features.V1.Authentication.Commands.ExternalLogin;
using Charity.Application.Features.V1.Authentication.Commands.ExternalLoginCallback;
using Charity.Application.Features.V1.Authentication.Commands.GenerateVerifyCode;
using Charity.Application.Features.V1.Authentication.Commands.LockAccount;
using Charity.Application.Features.V1.Authentication.Commands.Login;
using Charity.Application.Features.V1.Authentication.Commands.RefreshToken;
using Charity.Application.Features.V1.Authentication.Commands.Register;
using Charity.Application.Features.V1.Authentication.Commands.ResetPassword;
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
    public class AccountController : BaseApiController
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel createUser)
        {
            return NewResult(await Mediator.Send(new RegisterCommand(createUser)));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            return NewResult(await Mediator.Send(new LoginCommand(loginModel)));
        }

        [HttpGet("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin()
        {
            var properties = await Mediator.Send(new ExternalLoginCommand("Google", Url.Action(nameof(GoogleLoginCallback))));
            return Challenge(properties.Data!, "Google");
        }

        [HttpGet("GoogleLoginCallback")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            return NewResult(await Mediator.Send(new ExternalLoginCallbackCommand()));
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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmationRequest emailConfirmation)
        {
            return await Mediator.Send(new ConfirmEmailCommand(emailConfirmation));
        }

        [HttpPost("LockAccount")]
        public async Task<IActionResult> LockAccount([FromBody] UserEmailModel userEmail)
        {
            return NewResult(await Mediator.Send(new LockAccountCommand(userEmail)));
        }

        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnLockAccount([FromBody] UserEmailModel userEmail)
        {
            return NewResult(await Mediator.Send(new UnLockAccountCommand(userEmail)));
        }

    }
}
