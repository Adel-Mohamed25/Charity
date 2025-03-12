using Charity.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Charity.Contracts.RepositoryAbstractions.IdentityRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
    }
}
