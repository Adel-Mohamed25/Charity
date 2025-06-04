using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class AidDistribution : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string BeneficiaryId { get; set; }
        public CharityUser Beneficiary { get; set; }
        public string? MonetaryDonationId { get; set; }
        public MonetaryDonation? MonetaryDonation { get; set; }
        public string? InKindDonationId { get; set; }
        public InKindDonation? InKindDonation { get; set; }
        public string VolunteerId { get; set; }
        public CharityUser Volunteer { get; set; }
        public string Description { get; set; }
        public AidDistributionStatus Status { get; set; }
    }
}
