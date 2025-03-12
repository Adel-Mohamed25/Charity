using Charity.Contracts.RepositoryAbstractions.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Charity.Infrastructure.Implementations.IdentityRepository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ICharityDbContext context,
            RoleManager<Role> roleManager) : base(context)
        {
            RoleManager = roleManager;
        }
        public RoleManager<Role> RoleManager { get; }
    }
}
