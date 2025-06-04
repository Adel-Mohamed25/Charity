using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToMonetaryDonationandAidDistribution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_MonetaryDonations_DonorId",
                table: "MonetaryDonations",
                newName: "IX_MonetaryDonation_DonorId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "AidDistributions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AidDistribution_Status",
                table: "AidDistributions",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AidDistribution_Status",
                table: "AidDistributions");

            migrationBuilder.RenameIndex(
                name: "IX_MonetaryDonation_DonorId",
                table: "MonetaryDonations",
                newName: "IX_MonetaryDonations_DonorId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "AidDistributions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
