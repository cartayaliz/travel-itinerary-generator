using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Grupos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Paradas_Nombre",
                table: "Paradas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaquetesTuristicos_Nombre",
                table: "PaquetesTuristicos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_Nombre",
                table: "Itinerarios",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Paradas_Nombre",
                table: "Paradas");

            migrationBuilder.DropIndex(
                name: "IX_PaquetesTuristicos_Nombre",
                table: "PaquetesTuristicos");

            migrationBuilder.DropIndex(
                name: "IX_Itinerarios_Nombre",
                table: "Itinerarios");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Grupos");
        }
    }
}
