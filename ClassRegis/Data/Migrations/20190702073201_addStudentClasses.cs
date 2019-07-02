using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRegis.Data.Migrations
{
    public partial class addStudentClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    studentsId = table.Column<int>(nullable: false),
                    classId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyClasses_Classes_classId",
                        column: x => x.classId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyClasses_Students_studentsId",
                        column: x => x.studentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyClasses_classId",
                table: "StudyClasses",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyClasses_studentsId",
                table: "StudyClasses",
                column: "studentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyClasses");
        }
    }
}
