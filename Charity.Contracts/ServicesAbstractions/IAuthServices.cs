using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.Authentication;
using Charity.Models.Email;
using System.IdentityModel.Tokens.Jwt;

namespace Charity.Contracts.ServicesAbstractions
{
    public interface IUnitOfServices
    {
        Func<string, JwtSecurityToken, Task<bool>> IsTokenValidAsync { get; }

        Task<AuthModel> GetTokenAsync(User user);

        Task<JwtSecurityToken> ReadTokenAsync(string jwt);

        Task<AuthModel> GetRefreshTokenAsync(User user);

        Task<string> GenerateVerificationCodeAsync(User user);

        Task<bool> VerifyCodeAsync(User user, string code);

        Task<EmailConfirmationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailRequest);
    }
}
