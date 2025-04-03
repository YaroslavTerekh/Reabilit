using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorClassEntityAndModifyDoctorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Doctors",
                newName: "ExperienceInYear");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorClassId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DoctorClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorClassId",
                table: "Doctors",
                column: "DoctorClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorClasses_DoctorClassId",
                table: "Doctors",
                column: "DoctorClassId",
                principalTable: "DoctorClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorClasses_DoctorClassId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "DoctorClasses");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DoctorClassId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorClassId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "ExperienceInYear",
                table: "Doctors",
                newName: "Age");
        }
    }
}
