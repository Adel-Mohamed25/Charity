namespace Charity.Models.AidDistribution
{
    public class CreateAidDistributionModel
    {
        public string BeneficiaryId { get; set; }
        public string? MonetaryDonationId { get; set; }
        public string? InKindDonationId { get; set; }
        public string VolunteerId { get; set; }
        public string Description { get; set; }
    }
}
