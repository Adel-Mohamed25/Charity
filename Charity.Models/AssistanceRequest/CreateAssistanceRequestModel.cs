namespace Charity.Models.AssistanceRequest
{
    public class CreateAssistanceRequestModel
    {
        public string BeneficiaryId { get; set; }
        public string? RequestDetails { get; set; }
        public string? InKindDonationId { get; set; }
    }
}
