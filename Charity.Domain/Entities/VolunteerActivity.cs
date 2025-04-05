
using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class VolunteerActivity : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrganizerId { get; set; }
        public CharityUser Organizer { get; set; }
        public string ActivityDescription { get; set; }

        public ICollection<UserVolunteerActivity> Volunteers { get; set; } = new List<UserVolunteerActivity>();
        public ICollection<VolunteerApplication> VolunteerApplications { get; set; } = new List<VolunteerApplication>();

    }
}
