using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatWEB.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Glumac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glumac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reziser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reziser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Spoj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uloga = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spoj", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ocena = table.Column<float>(type: "real", nullable: false),
                    ZanrID = table.Column<int>(type: "int", nullable: true),
                    ReziserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Film_Reziser_ReziserID",
                        column: x => x.ReziserID,
                        principalTable: "Reziser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Film_Zanr_ZanrID",
                        column: x => x.ZanrID,
                        principalTable: "Zanr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Film_ReziserID",
                table: "Film",
                column: "ReziserID");

            migrationBuilder.CreateIndex(
                name: "IX_Film_ZanrID",
                table: "Film",
                column: "ZanrID");

            migrationBuilder.CreateIndex(
                name: "IX_FilmSpoj_FilmoviID",
                table: "FilmSpoj",
                column: "FilmoviID");

            migrationBuilder.CreateIndex(
                name: "IX_GlumacSpoj_GlumciID",
                table: "GlumacSpoj",
                column: "GlumciID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmSpoj");

            migrationBuilder.DropTable(
                name: "GlumacSpoj");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Glumac");

            migrationBuilder.DropTable(
                name: "Spoj");

            migrationBuilder.DropTable(
                name: "Reziser");

            migrationBuilder.DropTable(
                name: "Zanr");
        }
    }
}
