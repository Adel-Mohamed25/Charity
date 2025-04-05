using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class InKindDonation : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DonationItemType ItemType { get; set; } // "Clothes", "Food", "Medical Supplies"
        public string Description { get; set; }
        public int Quantity { get; set; }
        public IList<string>? ImageUrls { get; set; }
        public bool IsAllocated { get; set; }
        public string DonorId { get; set; }
        public CharityUser Donor { get; set; }
        public string? ProjectId { get; set; }
        public CharityProject? Project { get; set; }

        public ICollection<AssistanceRequest> AssistanceRequests { get; set; } = new List<AssistanceRequest>();
        public ICollection<AidDistribution> AidDistributions { get; set; } = new List<AidDistribution>();

    }
}
