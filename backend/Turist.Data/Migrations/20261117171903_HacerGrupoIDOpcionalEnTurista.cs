using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class HacerGrupoIDOpcionalEnTurista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios",
                column: "GrupoID",
                principalTable: "Grupos",
                principalColumn: "GrupoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios",
                column: "GrupoID",
                principalTable: "Grupos",
                principalColumn: "GrupoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
