using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class EbaMiMaikata3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleNameId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    NameId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
    }
}
