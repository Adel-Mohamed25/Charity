using Charity.Contracts.RepositoriesAbstraction;
using Charity.Contracts.RepositoriesAbstraction.IdentityRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Charity.Contracts.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ICharityUserRepository CharityUsers { get; }
        public IJwtTokenRepository JwtTokens { get; }
        public ICharityRoleRepository CharityRoles { get; }
        public IAidDistributionRepository AidDistributions { get; }
        public IAssistanceRequestRepository AssistanceRequests { get; }
        public IInKindDonationRepository InKindDonations { get; }
        public IMonetaryDonationRepository MonetaryDonations { get; }
        public IProjectVolunteerRepository ProjectVolunteers { get; }
        public INotificationRepository Notifications { get; }
        public IUserVolunteerActivityRepository UserVolunteerActivities { get; }
        public ICharityProjectRepository Projects { get; }
        public IVolunteerActivityRepository VolunteerActivities { get; }
        public IVolunteerApplicationRepository VolunteerApplications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
