using Charity.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Charity.Contracts.RepositoriesAbstraction.IdentityRepositories
{
    public interface ICharityUserRepository : IGenericRepository<CharityUser>
    {
        UserManager<CharityUser> UserManager { get; }
        SignInManager<CharityUser> SignInManager { get; }
    }
}
