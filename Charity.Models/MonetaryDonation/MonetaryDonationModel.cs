namespace Charity.Models.MonetaryDonation
{
    public class MonetaryDonationModel
    {
        public string Id { get; set; }
        public string DonorId { get; set; }
        public string? ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentIntentId { get; set; }
        public bool IsPaymentConfirmed { get; set; }

    }
}
