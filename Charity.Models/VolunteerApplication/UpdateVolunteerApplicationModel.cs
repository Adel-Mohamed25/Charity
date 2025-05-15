using Charity.Domain.Enum;

namespace Charity.Models.VolunteerApplication
{
    public class UpdateVolunteerApplicationModel
    {
        public string Id { get; set; }
        public string VolunteerId { get; set; }
        public string? RequestDetails { get; set; }
        public string? VolunteerActivityId { get; set; }
        public string? ProjectId { get; set; }
        public RequestStatus RequestStatus { get; set; }  // "Pending", "Approved", "Rejected"
    }
}
