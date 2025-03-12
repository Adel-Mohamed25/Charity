using Charity.Contracts.RepositoryAbstractions.IdentityRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Charity.Contracts.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository Users { get; }
        public IJwtTokenRepository JwtTokens { get; }
        public IRoleRepository Roles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
