namespace Charity.Models.MonetaryDonation
{
    public class CreatePaymentModel
    {
        public string DonorId { get; set; }
        public string? ProjectId { get; set; }
        public decimal Amount { get; set; }
    }
}
