using Charity.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charity.Domain.Entities.IdentityEntities
{
    public class User : IdentityUser<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int Age => DateOfBirth.HasValue
            ? (int)((DateTime.UtcNow - DateOfBirth.Value).TotalDays / 365.25)
            : 0;
        public string? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }


        [InverseProperty("User")]
        public ICollection<JwtToken> JwtTokens { get; set; } = new List<JwtToken>();
    }
}
