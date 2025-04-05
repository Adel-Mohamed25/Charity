using Charity.Contracts.Repositories;
using Charity.Contracts.RepositoriesAbstraction;
using Charity.Contracts.RepositoriesAbstraction.IdentityRepositories;
using Charity.Domain.Commons;
using Charity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Charity.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICharityDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        public ICharityUserRepository CharityUsers { get; private set; }
        public IJwtTokenRepository JwtTokens { get; private set; }
        public ICharityRoleRepository CharityRoles { get; private set; }
        public IAidDistributionRepository AidDistributions { get; private set; }
        public IAssistanceRequestRepository AssistanceRequests { get; private set; }
        public IInKindDonationRepository InKindDonations { get; private set; }
        public IMonetaryDonationRepository MonetaryDonations { get; private set; }
        public IProjectVolunteerRepository ProjectVolunteers { get; }
        public INotificationRepository Notifications { get; }
        public IUserVolunteerActivityRepository UserVolunteerActivities { get; }
        public ICharityProjectRepository Projects { get; private set; }
        public IVolunteerActivityRepository VolunteerActivities { get; private set; }
        public IVolunteerApplicationRepository VolunteerApplications { get; private set; }

        public UnitOfWork(ICharityDbContext context,
            ILogger<UnitOfWork> logger,
            ICharityUserRepository users,
            IJwtTokenRepository jwtTokens,
            ICharityRoleRepository roles,
            IAidDistributionRepository aidDistributions,
            IAssistanceRequestRepository assistanceRequests,
            IInKindDonationRepository inKindDonations,
            IMonetaryDonationRepository monetaryDonations,
            IProjectVolunteerRepository projectVolunteers,
            INotificationRepository notifications,
            IUserVolunteerActivityRepository userVolunteerActivities,
            ICharityProjectRepository projects,
            IVolunteerActivityRepository volunteerActivities,
            IVolunteerApplicationRepository volunteerApplications)
        {
            _context = context;
            _logger = logger;
            CharityUsers = users;
            JwtTokens = jwtTokens;
            CharityRoles = roles;
            AidDistributions = aidDistributions;
            AssistanceRequests = assistanceRequests;
            InKindDonations = inKindDonations;
            MonetaryDonations = monetaryDonations;
            ProjectVolunteers = projectVolunteers;
            Notifications = notifications;
            UserVolunteerActivities = userVolunteerActivities;
            Projects = projects;
            VolunteerActivities = volunteerActivities;
            VolunteerApplications = volunteerApplications;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries<BaseEntity<string>>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.Entity.ModifiedDate = DateTime.Now;
                            break;
                    }
                }

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes in UnitOfWork.");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => await _context.Database.BeginTransactionAsync(cancellationToken);


        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.CommitTransactionAsync(cancellationToken);


        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.RollbackTransactionAsync(cancellationToken);

    }
}
