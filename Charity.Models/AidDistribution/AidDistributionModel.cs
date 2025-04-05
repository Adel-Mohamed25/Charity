namespace Charity.Models.AidDistribution
{
    public class AidDistributionModel
    {
        public string Id { get; set; }
        public string BeneficiaryId { get; set; }
        public string? MonetaryDonationId { get; set; }
        public string? InKindDonationId { get; set; }
        public string VolunteerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
