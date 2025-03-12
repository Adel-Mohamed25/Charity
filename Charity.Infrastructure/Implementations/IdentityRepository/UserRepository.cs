using Charity.Contracts.RepositoryAbstractions.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Charity.Infrastructure.Implementations.IdentityRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ICharityDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager) : base(context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
    }
}
