using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CharityUsers_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CharityUsers_ReceiverId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Notifications");

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
                        name: "FK_UserNotifications_CharityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CharityUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CharityUsers_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "CharityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
