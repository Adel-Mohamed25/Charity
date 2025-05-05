using Charity.Domain.Entities;
using Charity.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Charity.Persistence.Context
{
    public class CharityDbContext : IdentityDbContext<CharityUser, CharityRole, string>, ICharityDbContext
    {
        public CharityDbContext(DbContextOptions<CharityDbContext> options) : base(options)
        {

        }

        public DbSet<CharityUser> CharityUsers { get; set; }
        public DbSet<CharityRole> CharityRoles { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<MonetaryDonation> MonetaryDonations { get; set; }
        public DbSet<InKindDonation> InKindDonations { get; set; }
        public DbSet<AssistanceRequest> AssistanceRequests { get; set; }
        public DbSet<AidDistribution> AidDistributions { get; set; }
        public DbSet<VolunteerActivity> VolunteerActivities { get; set; }
        public DbSet<UserVolunteerActivity> UserVolunteerActivities { get; set; }
        public DbSet<CharityProject> CharityProjects { get; set; }
        public DbSet<ProjectVolunteer> ProjectVolunteers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<VolunteerApplication> VolunteerApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
