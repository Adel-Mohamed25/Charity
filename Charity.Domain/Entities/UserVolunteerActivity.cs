using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class UserVolunteerActivity
    {
        public string UserId { get; set; }
        public CharityUser User { get; set; }

        public string VolunteerActivityId { get; set; }
        public VolunteerActivity VolunteerActivity { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
