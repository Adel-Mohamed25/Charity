using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatesInRelationshipAndAddVolunteerApplicationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_InKindDonations_InKindDonationId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_MonetaryDonations_MonetaryDonationId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_CharityProjects_ProjectId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_CharityProjects_ProjectId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_Users_DonorId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerActivities_Users_VolunteerId",
                table: "VolunteerActivities");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerActivities_VolunteerId",
                table: "VolunteerActivities");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "VolunteerActivities");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ParticipationDate",
                table: "ProjectVolunteers",
                newName: "JoinDate");

            migrationBuilder.AddColumn<string>(
                name: "OrganizerId",
                table: "VolunteerActivities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_UserNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserVolunteerActivities",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerActivityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVolunteerActivities", x => new { x.UserId, x.VolunteerActivityId });
                    table.ForeignKey(
                        name: "FK_UserVolunteerActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVolunteerActivities_VolunteerActivities_VolunteerActivityId",
                        column: x => x.VolunteerActivityId,
                        principalTable: "VolunteerActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerActivityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerApplications_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerApplications_VolunteerActivities_VolunteerActivityId",
                        column: x => x.VolunteerActivityId,
                        principalTable: "VolunteerActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActivities_OrganizerId",
                table: "VolunteerActivities",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVolunteerActivities_VolunteerActivityId",
                table: "UserVolunteerActivities",
                column: "VolunteerActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerApplications_VolunteerActivityId",
                table: "VolunteerApplications",
                column: "VolunteerActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerApplications_VolunteerId",
                table: "VolunteerApplications",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_InKindDonations_InKindDonationId",
                table: "AidDistributions",
                column: "InKindDonationId",
                principalTable: "InKindDonations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_MonetaryDonations_MonetaryDonationId",
                table: "AidDistributions",
                column: "MonetaryDonationId",
                principalTable: "MonetaryDonations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_CharityProjects_ProjectId",
                table: "InKindDonations",
                column: "ProjectId",
                principalTable: "CharityProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryDonations_CharityProjects_ProjectId",
                table: "MonetaryDonations",
                column: "ProjectId",
                principalTable: "CharityProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_VolunteerActivities_Users_OrganizerId",
                table: "VolunteerActivities",
                column: "OrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_InKindDonations_InKindDonationId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_MonetaryDonations_MonetaryDonationId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_CharityProjects_ProjectId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_CharityProjects_ProjectId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryDonations_Users_DonorId",
                table: "MonetaryDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerActivities_Users_OrganizerId",
                table: "VolunteerActivities");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "UserVolunteerActivities");

            migrationBuilder.DropTable(
                name: "VolunteerApplications");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerActivities_OrganizerId",
                table: "VolunteerActivities");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "VolunteerActivities");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "ProjectVolunteers",
                newName: "ParticipationDate");

            migrationBuilder.AddColumn<string>(
                name: "VolunteerId",
                table: "VolunteerActivities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActivities_VolunteerId",
                table: "VolunteerActivities",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_InKindDonations_InKindDonationId",
                table: "AidDistributions",
                column: "InKindDonationId",
                principalTable: "InKindDonations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_MonetaryDonations_MonetaryDonationId",
                table: "AidDistributions",
                column: "MonetaryDonationId",
                principalTable: "MonetaryDonations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AidDistributions_Users_VolunteerId",
                table: "AidDistributions",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_CharityProjects_ProjectId",
                table: "InKindDonations",
                column: "ProjectId",
                principalTable: "CharityProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindDonations_Users_DonorId",
                table: "InKindDonations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryDonations_CharityProjects_ProjectId",
                table: "MonetaryDonations",
                column: "ProjectId",
                principalTable: "CharityProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryDonations_Users_DonorId",
                table: "MonetaryDonations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerActivities_Users_VolunteerId",
                table: "VolunteerActivities",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
