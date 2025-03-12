using Charity.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Charity.Persistence.Context
{
    public interface ICharityDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<JwtToken> JwtTokens { get; }
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
