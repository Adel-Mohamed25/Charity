using Charity.Contracts.Repositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Infrastructure.Constans;

namespace Charity.Infrastructure.Seeds
{
    public static class RolesSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Roles.IsExistAsync())
            {
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = Roles.SuperAdmin.ToString() });
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = Roles.Admin.ToString() });
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = Roles.Basic.ToString() });
            }
        }
    }
}
