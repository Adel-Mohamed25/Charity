using Charity.Contracts.RepositoriesAbstraction.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Charity.Infrastructure.RepositoriesImplementation.IdentityRepository
{
    public class CharityRoleRepository : GenericRepository<CharityRole>, ICharityRoleRepository
    {
        public CharityRoleRepository(ICharityDbContext context,
            RoleManager<CharityRole> roleManager) : base(context)
        {
            RoleManager = roleManager;
        }
        public RoleManager<CharityRole> RoleManager { get; }
    }
}
