using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reabilit.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeInEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedule_Doctors_DoctorId",
                table: "DoctorSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSchedule",
                table: "DoctorSchedule");

            migrationBuilder.RenameTable(
                name: "DoctorSchedule",
                newName: "DoctorSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedule_DoctorId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DoctorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartsOn",
                table: "ProcedureEvents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorId",
                table: "DoctorSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules");

            migrationBuilder.RenameTable(
                name: "DoctorSchedules",
                newName: "DoctorSchedule");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedule",
                newName: "IX_DoctorSchedule_DoctorId");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartsOn",
                table: "ProcedureEvents",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSchedule",
                table: "DoctorSchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedule_Doctors_DoctorId",
                table: "DoctorSchedule",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
