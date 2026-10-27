using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_Usuario_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Usuario_TuristaID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_Usuario_GuiaID",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Usuario_GuiaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Usuario_TuristaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Paradas_Usuario_GuiaID",
                table: "Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_Usuario_TuristaID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Grupos_GrupoID",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Validaciones_Usuario_JefeID",
                table: "Validaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoraciones_Usuario_TuristaID",
                table: "Valoraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Usuario_TuristaID",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_GrupoID",
                table: "Usuarios",
                newName: "IX_Usuarios_GrupoID");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_Usuarios_GuiaID",
                table: "Guia_Paquete",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turista_Usuarios_TuristaID",
                table: "Itinerario_Turista",
                column: "TuristaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_Usuarios_GuiaID",
                table: "Itinerarios",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Usuarios_GuiaID",
                table: "Modificaciones",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Usuarios_TuristaID",
                table: "Modificaciones",
                column: "TuristaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paradas_Usuarios_GuiaID",
                table: "Paradas",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquete_Usuarios_TuristaID",
                table: "Turista_Paquete",
                column: "TuristaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios",
                column: "GrupoID",
                principalTable: "Grupos",
                principalColumn: "GrupoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validaciones_Usuarios_JefeID",
                table: "Validaciones",
                column: "JefeID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoraciones_Usuarios_TuristaID",
                table: "Valoraciones",
                column: "TuristaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Usuarios_TuristaID",
                table: "Visitas",
                column: "TuristaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_Usuarios_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Usuarios_TuristaID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_Usuarios_GuiaID",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Usuarios_GuiaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Usuarios_TuristaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Paradas_Usuarios_GuiaID",
                table: "Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_Usuarios_TuristaID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Grupos_GrupoID",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Validaciones_Usuarios_JefeID",
                table: "Validaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoraciones_Usuarios_TuristaID",
                table: "Valoraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Usuarios_TuristaID",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_GrupoID",
                table: "Usuario",
                newName: "IX_Usuario_GrupoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_Usuario_GuiaID",
                table: "Guia_Paquete",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turista_Usuario_TuristaID",
                table: "Itinerario_Turista",
                column: "TuristaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_Usuario_GuiaID",
                table: "Itinerarios",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Usuario_GuiaID",
                table: "Modificaciones",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Usuario_TuristaID",
                table: "Modificaciones",
                column: "TuristaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paradas_Usuario_GuiaID",
                table: "Paradas",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquete_Usuario_TuristaID",
                table: "Turista_Paquete",
                column: "TuristaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Grupos_GrupoID",
                table: "Usuario",
                column: "GrupoID",
                principalTable: "Grupos",
                principalColumn: "GrupoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validaciones_Usuario_JefeID",
                table: "Validaciones",
                column: "JefeID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoraciones_Usuario_TuristaID",
                table: "Valoraciones",
                column: "TuristaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Usuario_TuristaID",
                table: "Visitas",
                column: "TuristaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
