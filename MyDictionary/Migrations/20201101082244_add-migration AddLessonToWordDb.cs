using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDictionary.Migrations
{
    public partial class addmigrationAddLessonToWordDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.AddColumn<string>(
                name: "Lesson",
                table: "Words",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "Id");
        }
    }
}
