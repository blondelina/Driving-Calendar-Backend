using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingCalendar.Infrastructure.Migrations
{
    public partial class DrivingLessonUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLessons_StudentInstructors_StudentInstructorId",
                table: "DrivingLessons");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLessons_StudentInstructorId",
                table: "DrivingLessons");

            migrationBuilder.RenameColumn(
                name: "StudentStatus",
                table: "DrivingLessons",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "StudentInstructorId",
                table: "DrivingLessons",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "InstructorStatus",
                table: "DrivingLessons",
                newName: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLessons_InstructorId",
                table: "DrivingLessons",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLessons_StudentId",
                table: "DrivingLessons",
                column: "StudentId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_DrivingLessons_StudentId",
                table: "DrivingLessons");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "DrivingLessons",
                newName: "StudentStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DrivingLessons",
                newName: "StudentInstructorId");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "DrivingLessons",
                newName: "InstructorStatus");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLessons_StudentInstructorId",
                table: "DrivingLessons",
                column: "StudentInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLessons_StudentInstructors_StudentInstructorId",
                table: "DrivingLessons",
                column: "StudentInstructorId",
                principalTable: "StudentInstructors",
                principalColumn: "Id");
        }
    }
}
