using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class MonetaryDonation : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string DonorId { get; set; }
        public CharityUser Donor { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } // "Cash", "Credit Card", "Bank Transfer"
        public bool IsAllocated { get; set; } // هل تم تخصيصها لمشروع أو مستفيد؟
        public string? ProjectId { get; set; }
        public CharityProject Project { get; set; }
        public ICollection<AidDistribution> AidDistributions { get; set; } = new List<AidDistribution>();
    }
}
