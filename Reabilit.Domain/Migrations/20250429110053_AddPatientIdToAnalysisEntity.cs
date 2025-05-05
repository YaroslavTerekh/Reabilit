using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientIdToAnalysisEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Patients_PatientId",
                table: "Analyzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "Analyzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Patients_PatientId",
                table: "Analyzes",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Patients_PatientId",
                table: "Analyzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "Analyzes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Patients_PatientId",
                table: "Analyzes",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
