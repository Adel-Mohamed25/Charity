using Microsoft.AspNetCore.Identity;

namespace Charity.Domain.Entities.IdentityEntities
{
    public class CharityRole : IdentityRole<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
