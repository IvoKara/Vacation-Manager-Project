using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class TeamsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacantions_AspNetUsers_FromUserId",
                table: "Vacantions");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantions_AspNetUsers_FromUserId",
                table: "Vacantions",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacantions_AspNetUsers_FromUserId",
                table: "Vacantions");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantions_AspNetUsers_FromUserId",
                table: "Vacantions",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
