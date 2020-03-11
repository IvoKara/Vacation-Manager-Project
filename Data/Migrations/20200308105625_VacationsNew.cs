using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VacationsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Vacantions");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Vacantions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "Vacantions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Vacantions");

            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "Vacantions");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Vacantions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
