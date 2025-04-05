using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamesofRolesandUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_Users_BeneficiaryId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Roles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceRequests_Users_BeneficiaryId",
                table: "AssistanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityProjects_Users_ManagerId",
                table: "CharityProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_JwtTokens_Users_UserId",
                table: "JwtTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_Users_DonorId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectVolunteers_Users_VolunteerId",
                table: "ProjectVolunteers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Users_UserId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVolunteerActivities_Users_UserId",
                table: "UserVolunteerActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerActivities_Users_OrganizerId",
                table: "VolunteerActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerApplications_Users_VolunteerId",
                table: "VolunteerApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "CharityUsers");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "CharityRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityUsers",
                table: "CharityUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityRoles",
                table: "CharityRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_CharityUsers_BeneficiaryId",
                table: "AidDistributions",
                column: "BeneficiaryId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_CharityUsers_VolunteerId",
                table: "AidDistributions",
                column: "VolunteerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_CharityRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "CharityRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_CharityUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_CharityUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_CharityRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "CharityRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_CharityUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_CharityUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequests_CharityUsers_BeneficiaryId",
                table: "AssistanceRequests",
                column: "BeneficiaryId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityProjects_CharityUsers_ManagerId",
                table: "CharityProjects",
                column: "ManagerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_CharityUsers_DonorId",
                table: "InKindDonations",
                column: "DonorId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JwtTokens_CharityUsers_UserId",
                table: "JwtTokens",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryDonations_CharityUsers_DonorId",
                table: "MonetaryDonations",
                column: "DonorId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectVolunteers_CharityUsers_VolunteerId",
                table: "ProjectVolunteers",
                column: "VolunteerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_CharityUsers_UserId",
                table: "UserNotifications",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVolunteerActivities_CharityUsers_UserId",
                table: "UserVolunteerActivities",
                column: "UserId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerActivities_CharityUsers_OrganizerId",
                table: "VolunteerActivities",
                column: "OrganizerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications",
                column: "VolunteerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_CharityUsers_BeneficiaryId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_CharityUsers_VolunteerId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_CharityRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_CharityUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_CharityUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_CharityRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_CharityUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_CharityUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceRequests_CharityUsers_BeneficiaryId",
                table: "AssistanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityProjects_CharityUsers_ManagerId",
                table: "CharityProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_CharityUsers_DonorId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_JwtTokens_CharityUsers_UserId",
                table: "JwtTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_CharityUsers_DonorId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectVolunteers_CharityUsers_VolunteerId",
                table: "ProjectVolunteers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_CharityUsers_UserId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVolunteerActivities_CharityUsers_UserId",
                table: "UserVolunteerActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerActivities_CharityUsers_OrganizerId",
                table: "VolunteerActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityUsers",
                table: "CharityUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityRoles",
                table: "CharityRoles");

            migrationBuilder.RenameTable(
                name: "CharityUsers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "CharityRoles",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_Users_BeneficiaryId",
                table: "AidDistributions",
                column: "BeneficiaryId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Roles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequests_Users_BeneficiaryId",
                table: "AssistanceRequests",
                column: "BeneficiaryId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityProjects_Users_ManagerId",
                table: "CharityProjects",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JwtTokens_Users_UserId",
                table: "JwtTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryDonations_Users_DonorId",
                table: "MonetaryDonations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectVolunteers_Users_VolunteerId",
                table: "ProjectVolunteers",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Users_UserId",
                table: "UserNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVolunteerActivities_Users_UserId",
                table: "UserVolunteerActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerActivities_Users_OrganizerId",
                table: "VolunteerActivities",
                column: "OrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerApplications_Users_VolunteerId",
                table: "VolunteerApplications",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
