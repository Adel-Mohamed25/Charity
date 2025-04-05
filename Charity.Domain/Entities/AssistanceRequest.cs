using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class AssistanceRequest : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string BeneficiaryId { get; set; }
        public CharityUser Beneficiary { get; set; }
        public string? RequestDetails { get; set; }
        public string? InKindDonationId { get; set; }
        public InKindDonation? InKindDonation { get; set; }
        public RequestStatus RequestStatus { get; set; }  // "Pending", "Approved", "Rejected"

    }
}
