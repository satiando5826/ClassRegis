using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRegis.Data.Migrations
{
    public partial class AddTeacherEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

         
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

                         
        }
    }
}
