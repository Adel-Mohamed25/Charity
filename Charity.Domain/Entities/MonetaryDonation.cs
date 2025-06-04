using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class MonetaryDonation : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string DonorId { get; set; }
        public CharityUser Donor { get; set; }
        public string? ProjectId { get; set; }
        public CharityProject Project { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        public string PaymentIntentId { get; set; }

        public bool IsPaymentConfirmed { get; set; }

        public ICollection<AidDistribution> AidDistributions { get; set; } = new List<AidDistribution>();
    }
}
