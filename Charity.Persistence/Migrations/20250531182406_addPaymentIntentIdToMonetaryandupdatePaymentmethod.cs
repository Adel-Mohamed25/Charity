using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentIntentIdToMonetaryandupdatePaymentmethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllocated",
                table: "MonetaryDonations");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "MonetaryDonations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "MonetaryDonations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "MonetaryDonations");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "MonetaryDonations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllocated",
                table: "MonetaryDonations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
