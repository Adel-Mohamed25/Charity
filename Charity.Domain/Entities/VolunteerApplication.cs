using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class VolunteerApplication : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();

        public string VolunteerId { get; set; }
        public CharityUser Volunteer { get; set; }
        public string? RequestDetails { get; set; }
        public string? VolunteerActivityId { get; set; }
        public VolunteerActivity? VolunteerActivity { get; set; }
        public string? ProjectId { get; set; }
        public CharityProject? CharityProject { get; set; }

        public RequestStatus RequestStatus { get; set; }  // "Pending", "Approved", "Rejected"

    }
}
