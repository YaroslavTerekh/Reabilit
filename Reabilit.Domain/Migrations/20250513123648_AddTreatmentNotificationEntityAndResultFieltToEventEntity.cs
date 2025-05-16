using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTreatmentNotificationEntityAndResultFieltToEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "ProcedureEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TreatmentNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentNotification_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentNotification_AppUserId",
                table: "TreatmentNotification",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreatmentNotification");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "ProcedureEvents");
        }
    }
}
