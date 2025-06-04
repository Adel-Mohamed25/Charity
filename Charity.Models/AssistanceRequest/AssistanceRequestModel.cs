using Charity.Domain.Enum;

namespace Charity.Models.AssistanceRequest
{
    public class AssistanceRequestModel
    {
        public string Id { get; set; }
        public string BeneficiaryId { get; set; }
        public string? RequestDetails { get; set; }
        public string? InKindDonationId { get; set; }
        public RequestStatus RequestStatus { get; set; }  // "Pending", "Approved", "Rejected"

    }
}
