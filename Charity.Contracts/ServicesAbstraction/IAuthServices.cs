using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.Authentication;
using Charity.Models.Email;
using System.IdentityModel.Tokens.Jwt;

namespace Charity.Contracts.ServicesAbstraction
{
    public interface IUnitOfServices
    {
        Func<string, JwtSecurityToken, Task<bool>> IsTokenValidAsync { get; }

        Task<AuthModel> GetTokenAsync(CharityUser user);

        Task<JwtSecurityToken> ReadTokenAsync(string jwt);

        Task<AuthModel> GetRefreshTokenAsync(CharityUser user);

        Task<string> GenerateVerificationCodeAsync(CharityUser user);

        Task<bool> VerifyCodeAsync(CharityUser user, string code);

        Task<EmailConfirmationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailRequest);

    }
}
