using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class EbaMiMaikata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RoleNameId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    NameId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.NameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleNameId",
                table: "AspNetUsers",
                column: "RoleNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Role_RoleNameId",
                table: "AspNetUsers",
                column: "RoleNameId",
                principalTable: "Role",
                principalColumn: "NameId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Role_RoleNameId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleNameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleNameId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
