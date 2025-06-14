﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumNameToVolunteerActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VolunteerActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "VolunteerActivities");
        }
    }
}
