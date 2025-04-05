using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class ProjectVolunteer
    {
        public string ProjectId { get; set; }
        public CharityProject Project { get; set; }

        public string VolunteerId { get; set; }
        public CharityUser Volunteer { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
