using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialRealtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Projects_WorkingProjectId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkingProjectId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WorkingProjectId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkingOnProjectId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeaderId",
                table: "Teams",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkingOnProjectId",
                table: "Teams",
                column: "WorkingOnProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Projects_WorkingOnProjectId",
                table: "Teams",
                column: "WorkingOnProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Projects_WorkingOnProjectId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeaderId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkingOnProjectId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WorkingOnProjectId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "WorkingProjectId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkingProjectId",
                table: "Teams",
                column: "WorkingProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Projects_WorkingProjectId",
                table: "Teams",
                column: "WorkingProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
