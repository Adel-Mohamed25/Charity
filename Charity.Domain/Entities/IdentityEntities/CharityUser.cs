using Charity.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charity.Domain.Entities.IdentityEntities
{
    public class CharityUser : IdentityUser<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public UserRole UserType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public ICollection<JwtToken> JwtTokens { get; set; } = new List<JwtToken>();

        public ICollection<MonetaryDonation> MonetaryDonations { get; set; } = new List<MonetaryDonation>();

        public ICollection<InKindDonation> InKindDonations { get; set; } = new List<InKindDonation>();

        public ICollection<AssistanceRequest> AssistanceRequests { get; set; } = new List<AssistanceRequest>();

        [InverseProperty("Beneficiary")]
        public ICollection<AidDistribution> ReceivedAids { get; set; } = new List<AidDistribution>();

        [InverseProperty("Volunteer")]
        public ICollection<AidDistribution> DistributedAids { get; set; } = new List<AidDistribution>();

        public ICollection<UserVolunteerActivity> VolunteerActivities { get; set; } = new List<UserVolunteerActivity>();

        [InverseProperty("Organizer")]
        public ICollection<VolunteerActivity> OrganizedVolunteerActivities { get; set; } = new List<VolunteerActivity>();

        public ICollection<CharityProject> ManagedProjects { get; set; } = new List<CharityProject>();

        public ICollection<ProjectVolunteer> ProjectVolunteers { get; set; } = new List<ProjectVolunteer>();

        [InverseProperty("Sender")]
        public ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();
        public ICollection<UserNotification> ReceivedNotifications { get; set; } = new List<UserNotification>();
        public ICollection<VolunteerApplication> VolunteerApplications { get; set; } = new List<VolunteerApplication>();

    }
}
