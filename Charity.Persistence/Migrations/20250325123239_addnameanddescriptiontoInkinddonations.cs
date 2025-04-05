﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnameanddescriptiontoInkinddonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemType",
                table: "InKindDonations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InKindDonations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InKindDonations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "InKindDonations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InKindDonations");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "InKindDonations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
