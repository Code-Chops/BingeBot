using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingeBot.Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class BingeBot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tvmaze_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tvmaze_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    premiered_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShowGenre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    show_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowGenre", x => x.id);
                    table.ForeignKey(
                        name: "ForeignKey_Genre_Show",
                        column: x => x.show_id,
                        principalTable: "Shows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_id",
                table: "Persons",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_tvmaze_id",
                table: "Persons",
                column: "tvmaze_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenre_id",
                table: "ShowGenre",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenre_show_id",
                table: "ShowGenre",
                column: "show_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_id",
                table: "Shows",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shows_tvmaze_id",
                table: "Shows",
                column: "tvmaze_id",
                unique: true,
                filter: "[tvmaze_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "ShowGenre");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
