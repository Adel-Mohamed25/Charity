using Charity.Contracts.Repositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;
using Charity.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Charity.Infrastructure.Seeds
{
    public static class UsersSeeder
    {

        public static async Task SeedAdminUserAsync(IUnitOfWork unitOfWork)
        {
            var defaultAdminUser = new CharityUser
            {
                FirstName = "Adel",
                LastName = "Mohamed",
                UserName = "adel2852003adel@gmail.com",
                Email = "adel2852003adel@gmail.com",
                Address = "Menoufia",
                Gender = GenderType.Male,
                PhoneNumber = "01143254939",
                EmailConfirmed = true,
            };

            if (!await unitOfWork.CharityUsers.IsExistAsync(bu => bu.Email == defaultAdminUser.Email))
            {
                await unitOfWork.CharityUsers.UserManager.CreateAsync(defaultAdminUser, "1Q2w3e4@");
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, nameof(UserRole.Beneficiary));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, nameof(UserRole.Donor));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, nameof(UserRole.Volunteer));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, nameof(UserRole.Admin));
            }
        }

        public static async Task SeedSuperAdminUserAsync(IUnitOfWork unitOfWork)
        {
            var defaultSuperAdminUser = new CharityUser
            {
                FirstName = "Giving Hands",
                LastName = "Charity",
                UserName = "givinghands.contact@gmail.com",
                Email = "givinghands.contact@gmail.com",
                Address = "Menoufia",
                Gender = GenderType.Male,
                PhoneNumber = "01101426595",
                EmailConfirmed = true,
            };

            if (!await unitOfWork.CharityUsers.IsExistAsync(bu => bu.Email == defaultSuperAdminUser.Email))
            {
                await unitOfWork.CharityUsers.UserManager.CreateAsync(defaultSuperAdminUser, "1Q2w3e4@");
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, nameof(UserRole.Beneficiary));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, nameof(UserRole.Donor));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, nameof(UserRole.Volunteer));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, nameof(UserRole.Admin));
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, nameof(UserRole.SuperAdmin));
            }
            await unitOfWork.CharityRoles.RoleManager.SeedClaimsForSuperAdminAsync();
        }

        private static async Task SeedClaimsForSuperAdminAsync(this RoleManager<CharityRole> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(nameof(UserRole.SuperAdmin));
            await roleManager.AddPermissionClaimsAsync(superAdminRole!, "Beneficiary");
        }

        public static async Task AddPermissionClaimsAsync(this RoleManager<CharityRole> roleManager, CharityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }

    }
}
