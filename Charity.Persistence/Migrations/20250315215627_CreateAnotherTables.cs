using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateAnotherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Users",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CharityProjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityProjects_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerActivities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerActivities_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InKindDonations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InKindDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InKindDonations_CharityProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "CharityProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InKindDonations_Users_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryDonations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonetaryDonations_CharityProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "CharityProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonetaryDonations_Users_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVolunteers",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParticipationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVolunteers", x => new { x.ProjectId, x.VolunteerId });
                    table.ForeignKey(
                        name: "FK_ProjectVolunteers_CharityProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "CharityProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectVolunteers_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeneficiaryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InKindDonationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistanceRequests_InKindDonations_InKindDonationId",
                        column: x => x.InKindDonationId,
                        principalTable: "InKindDonations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssistanceRequests_Users_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AidDistributions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeneficiaryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonetaryDonationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InKindDonationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AidDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AidDistributions_InKindDonations_InKindDonationId",
                        column: x => x.InKindDonationId,
                        principalTable: "InKindDonations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AidDistributions_MonetaryDonations_MonetaryDonationId",
                        column: x => x.MonetaryDonationId,
                        principalTable: "MonetaryDonations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AidDistributions_Users_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AidDistributions_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AidDistributions_BeneficiaryId",
                table: "AidDistributions",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AidDistributions_InKindDonationId",
                table: "AidDistributions",
                column: "InKindDonationId");

            migrationBuilder.CreateIndex(
                name: "IX_AidDistributions_MonetaryDonationId",
                table: "AidDistributions",
                column: "MonetaryDonationId");

            migrationBuilder.CreateIndex(
                name: "IX_AidDistributions_VolunteerId",
                table: "AidDistributions",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_BeneficiaryId",
                table: "AssistanceRequests",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_InKindDonationId",
                table: "AssistanceRequests",
                column: "InKindDonationId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityProjects_ManagerId",
                table: "CharityProjects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_InKindDonations_DonorId",
                table: "InKindDonations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_InKindDonations_ProjectId",
                table: "InKindDonations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryDonations_DonorId",
                table: "MonetaryDonations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryDonations_ProjectId",
                table: "MonetaryDonations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVolunteers_VolunteerId",
                table: "ProjectVolunteers",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActivities_VolunteerId",
                table: "VolunteerActivities",
                column: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AidDistributions");

            migrationBuilder.DropTable(
                name: "AssistanceRequests");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ProjectVolunteers");

            migrationBuilder.DropTable(
                name: "VolunteerActivities");

            migrationBuilder.DropTable(
                name: "MonetaryDonations");

            migrationBuilder.DropTable(
                name: "InKindDonations");

            migrationBuilder.DropTable(
                name: "CharityProjects");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Users",
                newName: "Image");
        }
    }
}
