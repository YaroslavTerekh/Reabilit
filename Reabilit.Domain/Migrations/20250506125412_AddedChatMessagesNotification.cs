using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatMessagesNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageNotification_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageNotification_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotification_AppUserId",
                table: "MessageNotification",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotification_ChatMessageId",
                table: "MessageNotification",
                column: "ChatMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageNotification");
        }
    }
}
