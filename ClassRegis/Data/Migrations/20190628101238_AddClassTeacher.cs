using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRegis.Data.Migrations
{
    public partial class AddClassTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "teacherId",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_teacherId",
                table: "Classes",
                column: "teacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_teacherId",
                table: "Classes",
                column: "teacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_teacherId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_teacherId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "teacherId",
                table: "Classes");
        }
    }
}
