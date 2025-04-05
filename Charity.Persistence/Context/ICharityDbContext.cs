using Charity.Domain.Entities;
using Charity.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Charity.Persistence.Context
{
    public interface ICharityDbContext
    {
        DbSet<CharityUser> CharityUsers { get; }
        DbSet<CharityRole> CharityRoles { get; }
        DbSet<JwtToken> JwtTokens { get; }
        DbSet<MonetaryDonation> MonetaryDonations { get; set; }
        DbSet<InKindDonation> InKindDonations { get; set; }
        DbSet<AssistanceRequest> AssistanceRequests { get; set; }
        DbSet<AidDistribution> AidDistributions { get; set; }
        DbSet<VolunteerActivity> VolunteerActivities { get; set; }
        DbSet<UserVolunteerActivity> UserVolunteerActivities { get; set; }
        DbSet<CharityProject> CharityProjects { get; set; }
        DbSet<ProjectVolunteer> ProjectVolunteers { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        DbSet<VolunteerApplication> VolunteerApplications { get; set; }

        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
