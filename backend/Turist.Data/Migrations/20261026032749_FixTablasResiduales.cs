using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTablasResiduales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_Usuario_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Parada_Itinerarios_ItinerarioID",
                table: "Itinerario_Parada");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Parada_Paradas_ParadaID",
                table: "Itinerario_Parada");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Itinerarios_ItinerarioID",
                table: "Itinerario_Turista");

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
                name: "FK_Paquete_Categoria_Categorias_CategoriaID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_PaquetesTuristicos_PaqueteID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Paradas_Usuario_GuiaID",
                table: "Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Turista_Paquete");

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

            migrationBuilder.DropTable(
                name: "Actividad_Atraccion");

            migrationBuilder.DropTable(
                name: "Itinerario_Actividad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turista_Paquete",
                table: "Turista_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paquete_Categoria",
                table: "Paquete_Categoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Turista",
                table: "Itinerario_Turista");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Parada",
                table: "Itinerario_Parada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guia_Paquete",
                table: "Guia_Paquete");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Turista_Paquete",
                newName: "Turista_Paquetes");

            migrationBuilder.RenameTable(
                name: "Paquete_Categoria",
                newName: "Paquete_Categorias");

            migrationBuilder.RenameTable(
                name: "Itinerario_Turista",
                newName: "Itinerario_Turistas");

            migrationBuilder.RenameTable(
                name: "Itinerario_Parada",
                newName: "Itinerario_Paradas");

            migrationBuilder.RenameTable(
                name: "Guia_Paquete",
                newName: "Guia_Paquetes");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_GrupoID",
                table: "Usuarios",
                newName: "IX_Usuarios_GrupoID");

            migrationBuilder.RenameIndex(
                name: "IX_Turista_Paquete_PaqueteID",
                table: "Turista_Paquetes",
                newName: "IX_Turista_Paquetes_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Paquete_Categoria_CategoriaID",
                table: "Paquete_Categorias",
                newName: "IX_Paquete_Categorias_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Turista_TuristaID",
                table: "Itinerario_Turistas",
                newName: "IX_Itinerario_Turistas_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Parada_ParadaID",
                table: "Itinerario_Paradas",
                newName: "IX_Itinerario_Paradas_ParadaID");

            migrationBuilder.RenameIndex(
                name: "IX_Guia_Paquete_PaqueteID",
                table: "Guia_Paquetes",
                newName: "IX_Guia_Paquetes_PaqueteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turista_Paquetes",
                table: "Turista_Paquetes",
                columns: new[] { "TuristaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paquete_Categorias",
                table: "Paquete_Categorias",
                columns: new[] { "PaqueteID", "CategoriaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Turistas",
                table: "Itinerario_Turistas",
                columns: new[] { "ItinerarioID", "TuristaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Paradas",
                table: "Itinerario_Paradas",
                columns: new[] { "ItinerarioID", "ParadaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guia_Paquetes",
                table: "Guia_Paquetes",
                columns: new[] { "GuiaID", "PaqueteID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquetes_PaquetesTuristicos_PaqueteID",
                table: "Guia_Paquetes",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquetes_Usuarios_GuiaID",
                table: "Guia_Paquetes",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Paradas_Itinerarios_ItinerarioID",
                table: "Itinerario_Paradas",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Paradas_Paradas_ParadaID",
                table: "Itinerario_Paradas",
                column: "ParadaID",
                principalTable: "Paradas",
                principalColumn: "ParadaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turistas_Itinerarios_ItinerarioID",
                table: "Itinerario_Turistas",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turistas_Usuarios_TuristaID",
                table: "Itinerario_Turistas",
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
                name: "FK_Paquete_Categorias_Categorias_CategoriaID",
                table: "Paquete_Categorias",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paquete_Categorias_PaquetesTuristicos_PaqueteID",
                table: "Paquete_Categorias",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paradas_Usuarios_GuiaID",
                table: "Paradas",
                column: "GuiaID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquetes_PaquetesTuristicos_PaqueteID",
                table: "Turista_Paquetes",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquetes_Usuarios_TuristaID",
                table: "Turista_Paquetes",
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
                name: "FK_Guia_Paquetes_PaquetesTuristicos_PaqueteID",
                table: "Guia_Paquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquetes_Usuarios_GuiaID",
                table: "Guia_Paquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Paradas_Itinerarios_ItinerarioID",
                table: "Itinerario_Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Paradas_Paradas_ParadaID",
                table: "Itinerario_Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turistas_Itinerarios_ItinerarioID",
                table: "Itinerario_Turistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turistas_Usuarios_TuristaID",
                table: "Itinerario_Turistas");

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
                name: "FK_Paquete_Categorias_Categorias_CategoriaID",
                table: "Paquete_Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categorias_PaquetesTuristicos_PaqueteID",
                table: "Paquete_Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Paradas_Usuarios_GuiaID",
                table: "Paradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquetes_PaquetesTuristicos_PaqueteID",
                table: "Turista_Paquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquetes_Usuarios_TuristaID",
                table: "Turista_Paquetes");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turista_Paquetes",
                table: "Turista_Paquetes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paquete_Categorias",
                table: "Paquete_Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Turistas",
                table: "Itinerario_Turistas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Paradas",
                table: "Itinerario_Paradas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guia_Paquetes",
                table: "Guia_Paquetes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Turista_Paquetes",
                newName: "Turista_Paquete");

            migrationBuilder.RenameTable(
                name: "Paquete_Categorias",
                newName: "Paquete_Categoria");

            migrationBuilder.RenameTable(
                name: "Itinerario_Turistas",
                newName: "Itinerario_Turista");

            migrationBuilder.RenameTable(
                name: "Itinerario_Paradas",
                newName: "Itinerario_Parada");

            migrationBuilder.RenameTable(
                name: "Guia_Paquetes",
                newName: "Guia_Paquete");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_GrupoID",
                table: "Usuario",
                newName: "IX_Usuario_GrupoID");

            migrationBuilder.RenameIndex(
                name: "IX_Turista_Paquetes_PaqueteID",
                table: "Turista_Paquete",
                newName: "IX_Turista_Paquete_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Paquete_Categorias_CategoriaID",
                table: "Paquete_Categoria",
                newName: "IX_Paquete_Categoria_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Turistas_TuristaID",
                table: "Itinerario_Turista",
                newName: "IX_Itinerario_Turista_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Paradas_ParadaID",
                table: "Itinerario_Parada",
                newName: "IX_Itinerario_Parada_ParadaID");

            migrationBuilder.RenameIndex(
                name: "IX_Guia_Paquetes_PaqueteID",
                table: "Guia_Paquete",
                newName: "IX_Guia_Paquete_PaqueteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turista_Paquete",
                table: "Turista_Paquete",
                columns: new[] { "TuristaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paquete_Categoria",
                table: "Paquete_Categoria",
                columns: new[] { "PaqueteID", "CategoriaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Turista",
                table: "Itinerario_Turista",
                columns: new[] { "ItinerarioID", "TuristaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Parada",
                table: "Itinerario_Parada",
                columns: new[] { "ItinerarioID", "ParadaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guia_Paquete",
                table: "Guia_Paquete",
                columns: new[] { "GuiaID", "PaqueteID" });

            migrationBuilder.CreateTable(
                name: "Actividad_Atraccion",
                columns: table => new
                {
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    AtraccionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad_Atraccion", x => new { x.ActividadID, x.AtraccionID });
                    table.ForeignKey(
                        name: "FK_Actividad_Atraccion_Paradas_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Paradas",
                        principalColumn: "ParadaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividad_Atraccion_Paradas_AtraccionID",
                        column: x => x.AtraccionID,
                        principalTable: "Paradas",
                        principalColumn: "ParadaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerario_Actividad",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario_Actividad", x => new { x.ItinerarioID, x.ActividadID });
                    table.ForeignKey(
                        name: "FK_Itinerario_Actividad_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itinerario_Actividad_Paradas_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Paradas",
                        principalColumn: "ParadaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_Atraccion_AtraccionID",
                table: "Actividad_Atraccion",
                column: "AtraccionID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_Actividad_ActividadID",
                table: "Itinerario_Actividad",
                column: "ActividadID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Guia_Paquete",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_Usuario_GuiaID",
                table: "Guia_Paquete",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Parada_Itinerarios_ItinerarioID",
                table: "Itinerario_Parada",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Parada_Paradas_ParadaID",
                table: "Itinerario_Parada",
                column: "ParadaID",
                principalTable: "Paradas",
                principalColumn: "ParadaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turista_Itinerarios_ItinerarioID",
                table: "Itinerario_Turista",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
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
                name: "FK_Paquete_Categoria_Categorias_CategoriaID",
                table: "Paquete_Categoria",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paquete_Categoria_PaquetesTuristicos_PaqueteID",
                table: "Paquete_Categoria",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paradas_Usuario_GuiaID",
                table: "Paradas",
                column: "GuiaID",
                principalTable: "Usuario",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Turista_Paquete",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
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
