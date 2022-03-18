using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatWEB.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmSpoj");

            migrationBuilder.DropTable(
                name: "GlumacSpoj");

            migrationBuilder.AddColumn<int>(
                name: "FilmoviID",
                table: "Spoj",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GlumciID",
                table: "Spoj",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_FilmoviID",
                table: "Spoj",
                column: "FilmoviID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_GlumciID",
                table: "Spoj",
                column: "GlumciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spoj_Film_FilmoviID",
                table: "Spoj",
                column: "FilmoviID",
                principalTable: "Film",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spoj_Glumac_GlumciID",
                table: "Spoj",
                column: "GlumciID",
                principalTable: "Glumac",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spoj_Film_FilmoviID",
                table: "Spoj");

            migrationBuilder.DropForeignKey(
                name: "FK_Spoj_Glumac_GlumciID",
                table: "Spoj");

            migrationBuilder.DropIndex(
                name: "IX_Spoj_FilmoviID",
                table: "Spoj");

            migrationBuilder.DropIndex(
                name: "IX_Spoj_GlumciID",
                table: "Spoj");

            migrationBuilder.DropColumn(
                name: "FilmoviID",
                table: "Spoj");

            migrationBuilder.DropColumn(
                name: "GlumciID",
                table: "Spoj");

            migrationBuilder.CreateTable(
                name: "FilmSpoj",
                columns: table => new
                {
                    FilmGlumacID = table.Column<int>(type: "int", nullable: false),
                    FilmoviID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmSpoj", x => new { x.FilmGlumacID, x.FilmoviID });
                    table.ForeignKey(
                        name: "FK_FilmSpoj_Film_FilmoviID",
                        column: x => x.FilmoviID,
                        principalTable: "Film",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmSpoj_Spoj_FilmGlumacID",
                        column: x => x.FilmGlumacID,
                        principalTable: "Spoj",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlumacSpoj",
                columns: table => new
                {
                    FilmoviID = table.Column<int>(type: "int", nullable: false),
                    GlumciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlumacSpoj", x => new { x.FilmoviID, x.GlumciID });
                    table.ForeignKey(
                        name: "FK_GlumacSpoj_Glumac_GlumciID",
                        column: x => x.GlumciID,
                        principalTable: "Glumac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlumacSpoj_Spoj_FilmoviID",
                        column: x => x.FilmoviID,
                        principalTable: "Spoj",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmSpoj_FilmoviID",
                table: "FilmSpoj",
                column: "FilmoviID");

            migrationBuilder.CreateIndex(
                name: "IX_GlumacSpoj_GlumciID",
                table: "GlumacSpoj",
                column: "GlumciID");
        }
    }
}
