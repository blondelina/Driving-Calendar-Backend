using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingCalendar.Infrastructure.Migrations
{
    public partial class AddExamDateColumnToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExamDate",
                table: "Students",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamDate",
                table: "Students");
        }
    }
}
