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
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = nameof(UserRole.SuperAdmin) });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = nameof(UserRole.Admin) });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = nameof(UserRole.Beneficiary) });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = nameof(UserRole.Donor) });
                await unitOfWork.CharityRoles.RoleManager.CreateAsync(new CharityRole { Name = nameof(UserRole.Volunteer) });
            }
        }
    }
}
