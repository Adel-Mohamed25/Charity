using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Domain.Entities
{
    public class CharityProject : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal? TargetAmount { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }  // "Ongoing", "Completed", "Pending"
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ManagerId { get; set; }
        public CharityUser Manager { get; set; }
        public ICollection<MonetaryDonation> MonetaryDonations { get; set; } = new List<MonetaryDonation>();
        public ICollection<InKindDonation> InKindDonations { get; set; } = new List<InKindDonation>();
        public ICollection<ProjectVolunteer> ProjectVolunteers { get; set; } = new List<ProjectVolunteer>();
        public ICollection<VolunteerApplication> VolunteerApplications { get; set; } = new List<VolunteerApplication>();

    }
}
