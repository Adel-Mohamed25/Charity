using Charity.Domain.Enum;

namespace Charity.Models.AidDistribution
{
    public class AidDistributionModel
    {
        public string Id { get; set; }
        public string BeneficiaryId { get; set; }
        public string? MonetaryDonationId { get; set; }
        public string? InKindDonationId { get; set; }
        public string VolunteerId { get; set; }
        public string Description { get; set; }
        public AidDistributionStatus Status { get; set; }

        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
    }
}
