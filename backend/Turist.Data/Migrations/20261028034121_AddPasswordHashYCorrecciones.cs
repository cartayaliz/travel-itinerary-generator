using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashYCorrecciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Visitas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CantidadActividades",
                table: "Itinerarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoberturaCategorias",
                table: "Itinerarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origen",
                table: "Itinerarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProporcionExcursiones",
                table: "Itinerarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProporcionTalleres",
                table: "Itinerarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProporcionVisitas",
                table: "Itinerarios",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CantidadActividades",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "CoberturaCategorias",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "Origen",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "ProporcionExcursiones",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "ProporcionTalleres",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "ProporcionVisitas",
                table: "Itinerarios");
        }
    }
}
