using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatWEB.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KatalogID",
                table: "Spoj",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Katalog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katalog", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_KatalogID",
                table: "Spoj",
                column: "KatalogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spoj_Katalog_KatalogID",
                table: "Spoj",
                column: "KatalogID",
                principalTable: "Katalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spoj_Katalog_KatalogID",
                table: "Spoj");

            migrationBuilder.DropTable(
                name: "Katalog");

            migrationBuilder.DropIndex(
                name: "IX_Spoj_KatalogID",
                table: "Spoj");

            migrationBuilder.DropColumn(
                name: "KatalogID",
                table: "Spoj");
        }
    }
}
