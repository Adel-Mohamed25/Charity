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
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.Beneficiary.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.Donor.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.Volunteer.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.Admin.ToString());
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
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.Beneficiary.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.Donor.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.Volunteer.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.Admin.ToString());
                await unitOfWork.CharityUsers.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.SuperAdmin.ToString());
            }
            await unitOfWork.CharityRoles.RoleManager.SeedClaimsForSuperAdminAsync();
        }

        private static async Task SeedClaimsForSuperAdminAsync(this RoleManager<CharityRole> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(UserRole.SuperAdmin.ToString());
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
