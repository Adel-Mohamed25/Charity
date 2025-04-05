using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addVolunteerApplicationsConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VolunteerApplications");

            migrationBuilder.AlterColumn<string>(
                name: "VolunteerActivityId",
                table: "VolunteerApplications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "VolunteerApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestStatus",
                table: "VolunteerApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerApplications_ProjectId",
                table: "VolunteerApplications",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerApplications_CharityProjects_ProjectId",
                table: "VolunteerApplications",
                column: "ProjectId",
                principalTable: "CharityProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications",
                column: "VolunteerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerApplications_CharityProjects_ProjectId",
                table: "VolunteerApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerApplications_ProjectId",
                table: "VolunteerApplications");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "VolunteerApplications");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "VolunteerApplications");

            migrationBuilder.AlterColumn<string>(
                name: "VolunteerActivityId",
                table: "VolunteerApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VolunteerApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerApplications_CharityUsers_VolunteerId",
                table: "VolunteerApplications",
                column: "VolunteerId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
