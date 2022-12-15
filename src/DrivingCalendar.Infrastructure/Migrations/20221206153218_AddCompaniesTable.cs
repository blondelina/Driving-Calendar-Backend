using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingCalendar.Infrastructure.Migrations
{
    public partial class AddCompaniesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_CompanyId",
                table: "Instructors",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Companies_CompanyId",
                table: "Instructors",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Companies_CompanyId",
                table: "Instructors");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_CompanyId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Instructors");
        }
    }
}
