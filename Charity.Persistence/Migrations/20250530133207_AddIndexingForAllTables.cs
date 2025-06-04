using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexingForAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_VolunteerApplications_VolunteerActivityId",
                table: "VolunteerApplications",
                newName: "IX_VolunteerApplication_VolunteerActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerApplications_ProjectId",
                table: "VolunteerApplications",
                newName: "IX_VolunteerApplication_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVolunteerActivities_VolunteerActivityId",
                table: "UserVolunteerActivities",
                newName: "IX_UserVolunteerActivity_VolunteerActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectVolunteers_VolunteerId",
                table: "ProjectVolunteers",
                newName: "IX_ProjectVolunteer_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                newName: "IX_Notification_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                newName: "IX_Notification_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityProjects_Name",
                table: "CharityProjects",
                newName: "IX_CharityProject_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AssistanceRequests_BeneficiaryId",
                table: "AssistanceRequests",
                newName: "IX_AssistanceRequest_BeneficiaryId");

            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "VolunteerApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VolunteerActivities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "AssistanceRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerApplication_RequestStatus",
                table: "VolunteerApplications",
                column: "RequestStatus");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActivity_Name",
                table: "VolunteerActivities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UserVolunteerActivity_UserId",
                table: "UserVolunteerActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVolunteer_ProjectId",
                table: "ProjectVolunteers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedDate",
                table: "Notifications",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CharityUser_FirstName",
                table: "CharityUsers",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_RequestStatus",
                table: "AssistanceRequests",
                column: "RequestStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VolunteerApplication_RequestStatus",
                table: "VolunteerApplications");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerActivity_Name",
                table: "VolunteerActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserVolunteerActivity_UserId",
                table: "UserVolunteerActivities");

            migrationBuilder.DropIndex(
                name: "IX_ProjectVolunteer_ProjectId",
                table: "ProjectVolunteers");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CreatedDate",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_CharityUser_FirstName",
                table: "CharityUsers");

            migrationBuilder.DropIndex(
                name: "IX_AssistanceRequest_RequestStatus",
                table: "AssistanceRequests");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerApplication_VolunteerActivityId",
                table: "VolunteerApplications",
                newName: "IX_VolunteerApplications_VolunteerActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerApplication_ProjectId",
                table: "VolunteerApplications",
                newName: "IX_VolunteerApplications_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVolunteerActivity_VolunteerActivityId",
                table: "UserVolunteerActivities",
                newName: "IX_UserVolunteerActivities_VolunteerActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectVolunteer_VolunteerId",
                table: "ProjectVolunteers",
                newName: "IX_ProjectVolunteers_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_SenderId",
                table: "Notifications",
                newName: "IX_Notifications_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_ReceiverId",
                table: "Notifications",
                newName: "IX_Notifications_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityProject_Name",
                table: "CharityProjects",
                newName: "IX_CharityProjects_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AssistanceRequest_BeneficiaryId",
                table: "AssistanceRequests",
                newName: "IX_AssistanceRequests_BeneficiaryId");

            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "VolunteerApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VolunteerActivities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "AssistanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
