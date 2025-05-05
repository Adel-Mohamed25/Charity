using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDonationStatusInInKindDonation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DonationStatus",
                table: "InKindDonations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DonationStatus",
                table: "InKindDonations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
