using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserTypeFromCharityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "CharityUsers");

            migrationBuilder.AddColumn<int>(
                name: "DonationStatus",
                table: "InKindDonations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationStatus",
                table: "InKindDonations");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "CharityUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
