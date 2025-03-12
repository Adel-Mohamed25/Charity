using Charity.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charity.Domain.Entities.IdentityEntities
{
    public class JwtToken : BaseEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string JWT { get; set; }
        public string RefreshJWT { get; set; }
        public DateTime JWTExpireDate { get; set; }
        public DateTime RefreshJWTExpireDate { get; set; }
        public DateTime? RefreshJWTRevokedDate { get; set; }
        public bool IsRefreshJWTUsed { get; set; }
        public bool IsRefreshJWTExpired => DateTime.UtcNow >= RefreshJWTExpireDate;
        public bool IsRefreshJWTActive => RefreshJWTRevokedDate == null && !IsRefreshJWTExpired;

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.JwtTokens))]
        public virtual User User { get; set; }
    }
}
