using Charity.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Charity.Contracts.RepositoriesAbstraction.IdentityRepositories
{
    public interface ICharityRoleRepository : IGenericRepository<CharityRole>
    {
        RoleManager<CharityRole> RoleManager { get; }
    }
}
