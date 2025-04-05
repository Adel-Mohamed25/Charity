using Charity.Contracts.Repositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;

namespace Charity.Infrastructure.Seeds
{
    public static class RolesSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.CharityRoles.IsExistAsync())
            {
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = UserRole.SuperAdmin.ToString() });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = UserRole.Admin.ToString() });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = UserRole.Beneficiary.ToString() });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = UserRole.Donor.ToString() });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = UserRole.Volunteer.ToString() });
            }
        }
    }
}
