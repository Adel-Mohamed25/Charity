using Charity.Contracts.RepositoriesAbstraction.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Charity.Infrastructure.RepositoriesImplementation.IdentityRepository
{
    public class CharityUserRepository : GenericRepository<CharityUser>, ICharityUserRepository
    {
        public CharityUserRepository(ICharityDbContext context,
            UserManager<CharityUser> userManager,
            SignInManager<CharityUser> signInManager) : base(context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<CharityUser> UserManager { get; }
        public SignInManager<CharityUser> SignInManager { get; }
    }
}
