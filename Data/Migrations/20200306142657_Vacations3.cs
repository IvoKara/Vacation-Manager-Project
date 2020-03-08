using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Vacations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       /*     migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_LeaderId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_LeaderId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
