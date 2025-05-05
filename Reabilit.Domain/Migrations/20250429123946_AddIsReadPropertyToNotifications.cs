using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadPropertyToNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ProcedureEventNotification",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ProcedureEventNotification");
        }
    }
}
