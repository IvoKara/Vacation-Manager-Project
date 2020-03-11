using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VacationsCRUDReady : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Editted",
                table: "Vacantions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Editted",
                table: "Vacantions");
        }
    }
}
