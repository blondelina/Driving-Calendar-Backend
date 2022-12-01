using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingCalendar.Infrastructure.Migrations
{
    public partial class ChangeDrivingLessonsToReferenceInstructorsStudentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLessons_Instructors_InstructorId",
                table: "DrivingLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLessons_Students_StudentId",
                table: "DrivingLessons");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLessons_InstructorId",
                table: "DrivingLessons");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "DrivingLessons");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "DrivingLessons",
                newName: "StudentInstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingLessons_StudentId",
                table: "DrivingLessons",
                newName: "IX_DrivingLessons_StudentInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLessons_StudentInstructors_StudentInstructorId",
                table: "DrivingLessons",
                column: "StudentInstructorId",
                principalTable: "StudentInstructors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLessons_StudentInstructors_StudentInstructorId",
                table: "DrivingLessons");

            migrationBuilder.RenameColumn(
                name: "StudentInstructorId",
                table: "DrivingLessons",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingLessons_StudentInstructorId",
                table: "DrivingLessons",
                newName: "IX_DrivingLessons_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "DrivingLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLessons_InstructorId",
                table: "DrivingLessons",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLessons_Instructors_InstructorId",
                table: "DrivingLessons",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLessons_Students_StudentId",
                table: "DrivingLessons",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
