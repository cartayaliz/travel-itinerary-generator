using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResultadoToModificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Modificaciones",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Modificaciones");
        }
    }
}
