using Charity.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Charity.Contracts.RepositoryAbstractions.IdentityRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        RoleManager<Role> RoleManager { get; }
    }
}
