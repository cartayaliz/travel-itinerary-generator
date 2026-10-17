using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorregirNombresTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadAtracciones_Actividades_ActividadID",
                table: "ActividadAtracciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ActividadAtracciones_Atracciones_AtraccionID",
                table: "ActividadAtracciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Categorias_CategoriaID",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_GuiasTuristicos_GuiaID",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Atracciones_Categorias_CategoriaID",
                table: "Atracciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Atracciones_GuiasTuristicos_GuiaID",
                table: "Atracciones");

            migrationBuilder.DropForeignKey(
                name: "FK_GuiaPaquetes_GuiasTuristicos_GuiaID",
                table: "GuiaPaquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_GuiaPaquetes_PaquetesTuristicos_PaqueteID",
                table: "GuiaPaquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_ItinerarioActividades_Actividades_ActividadID",
                table: "ItinerarioActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ItinerarioActividades_Itinerarios_ItinerarioID",
                table: "ItinerarioActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_GuiasTuristicos_GuiaID",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_PaquetesTuristicos_PaqueteID",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ItinerarioTuristas_Itinerarios_ItinerarioID",
                table: "ItinerarioTuristas");

            migrationBuilder.DropForeignKey(
                name: "FK_ItinerarioTuristas_Turistas_TuristaID",
                table: "ItinerarioTuristas");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_GuiasTuristicos_GuiaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Itinerarios_ItinerarioID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificaciones_Turistas_TuristaID",
                table: "Modificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_PaqueteCategorias_Categorias_CategoriaID",
                table: "PaqueteCategorias");

            migrationBuilder.DropForeignKey(
                name: "FK_PaqueteCategorias_PaquetesTuristicos_PaqueteID",
                table: "PaqueteCategorias");

            migrationBuilder.DropForeignKey(
                name: "FK_TuristaPaquetes_PaquetesTuristicos_PaqueteID",
                table: "TuristaPaquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_TuristaPaquetes_Turistas_TuristaID",
                table: "TuristaPaquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Turistas_Grupos_GrupoID",
                table: "Turistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Validaciones_GuiasTuristicos_GuiaID",
                table: "Validaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Validaciones_Itinerarios_ItinerarioID",
                table: "Validaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoraciones_Itinerarios_ItinerarioID",
                table: "Valoraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoraciones_Turistas_TuristaID",
                table: "Valoraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Actividades_ActividadID",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Itinerarios_ItinerarioID",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Modificaciones_ModificacionID",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Turistas_TuristaID",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Validaciones_ValidacionID",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Valoraciones_ValoracionID",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Valoraciones",
                table: "Valoraciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Validaciones",
                table: "Validaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turistas",
                table: "Turistas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TuristaPaquetes",
                table: "TuristaPaquetes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaquetesTuristicos",
                table: "PaquetesTuristicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaqueteCategorias",
                table: "PaqueteCategorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modificaciones",
                table: "Modificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItinerarioTuristas",
                table: "ItinerarioTuristas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerarios",
                table: "Itinerarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItinerarioActividades",
                table: "ItinerarioActividades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuiasTuristicos",
                table: "GuiasTuristicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuiaPaquetes",
                table: "GuiaPaquetes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grupos",
                table: "Grupos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atracciones",
                table: "Atracciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actividades",
                table: "Actividades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActividadAtracciones",
                table: "ActividadAtracciones");

            migrationBuilder.RenameTable(
                name: "Visitas",
                newName: "Visita");

            migrationBuilder.RenameTable(
                name: "Valoraciones",
                newName: "Valoracion");

            migrationBuilder.RenameTable(
                name: "Validaciones",
                newName: "Validacion");

            migrationBuilder.RenameTable(
                name: "Turistas",
                newName: "Turista");

            migrationBuilder.RenameTable(
                name: "TuristaPaquetes",
                newName: "Turista_Paquete");

            migrationBuilder.RenameTable(
                name: "PaquetesTuristicos",
                newName: "PaqueteTuristico");

            migrationBuilder.RenameTable(
                name: "PaqueteCategorias",
                newName: "Paquete_Categoria");

            migrationBuilder.RenameTable(
                name: "Modificaciones",
                newName: "Modificacion");

            migrationBuilder.RenameTable(
                name: "ItinerarioTuristas",
                newName: "Itinerario_Turista");

            migrationBuilder.RenameTable(
                name: "Itinerarios",
                newName: "Itinerario");

            migrationBuilder.RenameTable(
                name: "ItinerarioActividades",
                newName: "Itinerario_Actividad");

            migrationBuilder.RenameTable(
                name: "GuiasTuristicos",
                newName: "GuiaTuristico");

            migrationBuilder.RenameTable(
                name: "GuiaPaquetes",
                newName: "Guia_Paquete");

            migrationBuilder.RenameTable(
                name: "Grupos",
                newName: "Grupo");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categoria");

            migrationBuilder.RenameTable(
                name: "Atracciones",
                newName: "Atraccion");

            migrationBuilder.RenameTable(
                name: "Actividades",
                newName: "Actividad");

            migrationBuilder.RenameTable(
                name: "ActividadAtracciones",
                newName: "Actividad_Atraccion");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_ValoracionID",
                table: "Visita",
                newName: "IX_Visita_ValoracionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_ValidacionID",
                table: "Visita",
                newName: "IX_Visita_ValidacionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_TuristaID",
                table: "Visita",
                newName: "IX_Visita_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_ModificacionID",
                table: "Visita",
                newName: "IX_Visita_ModificacionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_ItinerarioID",
                table: "Visita",
                newName: "IX_Visita_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_ActividadID",
                table: "Visita",
                newName: "IX_Visita_ActividadID");

            migrationBuilder.RenameIndex(
                name: "IX_Valoraciones_TuristaID",
                table: "Valoracion",
                newName: "IX_Valoracion_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Valoraciones_ItinerarioID",
                table: "Valoracion",
                newName: "IX_Valoracion_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Validaciones_ItinerarioID",
                table: "Validacion",
                newName: "IX_Validacion_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Validaciones_GuiaID",
                table: "Validacion",
                newName: "IX_Validacion_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Turistas_GrupoID",
                table: "Turista",
                newName: "IX_Turista_GrupoID");

            migrationBuilder.RenameIndex(
                name: "IX_TuristaPaquetes_PaqueteID",
                table: "Turista_Paquete",
                newName: "IX_Turista_Paquete_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_PaqueteCategorias_CategoriaID",
                table: "Paquete_Categoria",
                newName: "IX_Paquete_Categoria_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificaciones_TuristaID",
                table: "Modificacion",
                newName: "IX_Modificacion_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificaciones_ItinerarioID",
                table: "Modificacion",
                newName: "IX_Modificacion_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificaciones_GuiaID",
                table: "Modificacion",
                newName: "IX_Modificacion_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_ItinerarioTuristas_TuristaID",
                table: "Itinerario_Turista",
                newName: "IX_Itinerario_Turista_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_PaqueteID",
                table: "Itinerario",
                newName: "IX_Itinerario_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_GuiaID",
                table: "Itinerario",
                newName: "IX_Itinerario_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_ItinerarioActividades_ActividadID",
                table: "Itinerario_Actividad",
                newName: "IX_Itinerario_Actividad_ActividadID");

            migrationBuilder.RenameIndex(
                name: "IX_GuiaPaquetes_PaqueteID",
                table: "Guia_Paquete",
                newName: "IX_Guia_Paquete_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Atracciones_GuiaID",
                table: "Atraccion",
                newName: "IX_Atraccion_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Atracciones_CategoriaID",
                table: "Atraccion",
                newName: "IX_Atraccion_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_GuiaID",
                table: "Actividad",
                newName: "IX_Actividad_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_CategoriaID",
                table: "Actividad",
                newName: "IX_Actividad_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_ActividadAtracciones_AtraccionID",
                table: "Actividad_Atraccion",
                newName: "IX_Actividad_Atraccion_AtraccionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visita",
                table: "Visita",
                column: "VisitaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Valoracion",
                table: "Valoracion",
                column: "ValoracionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Validacion",
                table: "Validacion",
                column: "ValidacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turista",
                table: "Turista",
                column: "TuristaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turista_Paquete",
                table: "Turista_Paquete",
                columns: new[] { "TuristaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaqueteTuristico",
                table: "PaqueteTuristico",
                column: "PaqueteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paquete_Categoria",
                table: "Paquete_Categoria",
                columns: new[] { "PaqueteID", "CategoriaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modificacion",
                table: "Modificacion",
                column: "ModificacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Turista",
                table: "Itinerario_Turista",
                columns: new[] { "ItinerarioID", "TuristaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario",
                table: "Itinerario",
                column: "ItinerarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_Actividad",
                table: "Itinerario_Actividad",
                columns: new[] { "ItinerarioID", "ActividadID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuiaTuristico",
                table: "GuiaTuristico",
                column: "GuiaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guia_Paquete",
                table: "Guia_Paquete",
                columns: new[] { "GuiaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo",
                column: "GrupoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "CategoriaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atraccion",
                table: "Atraccion",
                column: "AtraccionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actividad",
                table: "Actividad",
                column: "ActividadID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actividad_Atraccion",
                table: "Actividad_Atraccion",
                columns: new[] { "ActividadID", "AtraccionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Categoria_CategoriaID",
                table: "Actividad",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_GuiaTuristico_GuiaID",
                table: "Actividad",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Atraccion_Actividad_ActividadID",
                table: "Actividad_Atraccion",
                column: "ActividadID",
                principalTable: "Actividad",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Atraccion_Atraccion_AtraccionID",
                table: "Actividad_Atraccion",
                column: "AtraccionID",
                principalTable: "Atraccion",
                principalColumn: "AtraccionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atraccion_Categoria_CategoriaID",
                table: "Atraccion",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atraccion_GuiaTuristico_GuiaID",
                table: "Atraccion",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_GuiaTuristico_GuiaID",
                table: "Guia_Paquete",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guia_Paquete_PaqueteTuristico_PaqueteID",
                table: "Guia_Paquete",
                column: "PaqueteID",
                principalTable: "PaqueteTuristico",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_GuiaTuristico_GuiaID",
                table: "Itinerario",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PaqueteTuristico_PaqueteID",
                table: "Itinerario",
                column: "PaqueteID",
                principalTable: "PaqueteTuristico",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Actividad_Actividad_ActividadID",
                table: "Itinerario_Actividad",
                column: "ActividadID",
                principalTable: "Actividad",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Actividad_Itinerario_ItinerarioID",
                table: "Itinerario_Actividad",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turista_Itinerario_ItinerarioID",
                table: "Itinerario_Turista",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Turista_Turista_TuristaID",
                table: "Itinerario_Turista",
                column: "TuristaID",
                principalTable: "Turista",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificacion_GuiaTuristico_GuiaID",
                table: "Modificacion",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modificacion_Itinerario_ItinerarioID",
                table: "Modificacion",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificacion_Turista_TuristaID",
                table: "Modificacion",
                column: "TuristaID",
                principalTable: "Turista",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paquete_Categoria_Categoria_CategoriaID",
                table: "Paquete_Categoria",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paquete_Categoria_PaqueteTuristico_PaqueteID",
                table: "Paquete_Categoria",
                column: "PaqueteID",
                principalTable: "PaqueteTuristico",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Grupo_GrupoID",
                table: "Turista",
                column: "GrupoID",
                principalTable: "Grupo",
                principalColumn: "GrupoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquete_PaqueteTuristico_PaqueteID",
                table: "Turista_Paquete",
                column: "PaqueteID",
                principalTable: "PaqueteTuristico",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turista_Paquete_Turista_TuristaID",
                table: "Turista_Paquete",
                column: "TuristaID",
                principalTable: "Turista",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_GuiaTuristico_GuiaID",
                table: "Validacion",
                column: "GuiaID",
                principalTable: "GuiaTuristico",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_Itinerario_ItinerarioID",
                table: "Validacion",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoracion_Itinerario_ItinerarioID",
                table: "Valoracion",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoracion_Turista_TuristaID",
                table: "Valoracion",
                column: "TuristaID",
                principalTable: "Turista",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Actividad_ActividadID",
                table: "Visita",
                column: "ActividadID",
                principalTable: "Actividad",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Itinerario_ItinerarioID",
                table: "Visita",
                column: "ItinerarioID",
                principalTable: "Itinerario",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Modificacion_ModificacionID",
                table: "Visita",
                column: "ModificacionID",
                principalTable: "Modificacion",
                principalColumn: "ModificacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Turista_TuristaID",
                table: "Visita",
                column: "TuristaID",
                principalTable: "Turista",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Validacion_ValidacionID",
                table: "Visita",
                column: "ValidacionID",
                principalTable: "Validacion",
                principalColumn: "ValidacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Valoracion_ValoracionID",
                table: "Visita",
                column: "ValoracionID",
                principalTable: "Valoracion",
                principalColumn: "ValoracionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Categoria_CategoriaID",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_GuiaTuristico_GuiaID",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Actividad_ActividadID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Atraccion_AtraccionID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Atraccion_Categoria_CategoriaID",
                table: "Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Atraccion_GuiaTuristico_GuiaID",
                table: "Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_GuiaTuristico_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_PaqueteTuristico_PaqueteID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_GuiaTuristico_GuiaID",
                table: "Itinerario");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PaqueteTuristico_PaqueteID",
                table: "Itinerario");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Actividad_Actividad_ActividadID",
                table: "Itinerario_Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Actividad_Itinerario_ItinerarioID",
                table: "Itinerario_Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Itinerario_ItinerarioID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Turista_TuristaID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificacion_GuiaTuristico_GuiaID",
                table: "Modificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificacion_Itinerario_ItinerarioID",
                table: "Modificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Modificacion_Turista_TuristaID",
                table: "Modificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_Categoria_CategoriaID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_PaqueteTuristico_PaqueteID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Grupo_GrupoID",
                table: "Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_PaqueteTuristico_PaqueteID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_Turista_TuristaID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_GuiaTuristico_GuiaID",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_Itinerario_ItinerarioID",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoracion_Itinerario_ItinerarioID",
                table: "Valoracion");

            migrationBuilder.DropForeignKey(
                name: "FK_Valoracion_Turista_TuristaID",
                table: "Valoracion");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Actividad_ActividadID",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Itinerario_ItinerarioID",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Modificacion_ModificacionID",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Turista_TuristaID",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Validacion_ValidacionID",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Valoracion_ValoracionID",
                table: "Visita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visita",
                table: "Visita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Valoracion",
                table: "Valoracion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Validacion",
                table: "Validacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turista_Paquete",
                table: "Turista_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turista",
                table: "Turista");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaqueteTuristico",
                table: "PaqueteTuristico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paquete_Categoria",
                table: "Paquete_Categoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modificacion",
                table: "Modificacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Turista",
                table: "Itinerario_Turista");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_Actividad",
                table: "Itinerario_Actividad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario",
                table: "Itinerario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuiaTuristico",
                table: "GuiaTuristico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guia_Paquete",
                table: "Guia_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atraccion",
                table: "Atraccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actividad_Atraccion",
                table: "Actividad_Atraccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actividad",
                table: "Actividad");

            migrationBuilder.RenameTable(
                name: "Visita",
                newName: "Visitas");

            migrationBuilder.RenameTable(
                name: "Valoracion",
                newName: "Valoraciones");

            migrationBuilder.RenameTable(
                name: "Validacion",
                newName: "Validaciones");

            migrationBuilder.RenameTable(
                name: "Turista_Paquete",
                newName: "TuristaPaquetes");

            migrationBuilder.RenameTable(
                name: "Turista",
                newName: "Turistas");

            migrationBuilder.RenameTable(
                name: "PaqueteTuristico",
                newName: "PaquetesTuristicos");

            migrationBuilder.RenameTable(
                name: "Paquete_Categoria",
                newName: "PaqueteCategorias");

            migrationBuilder.RenameTable(
                name: "Modificacion",
                newName: "Modificaciones");

            migrationBuilder.RenameTable(
                name: "Itinerario_Turista",
                newName: "ItinerarioTuristas");

            migrationBuilder.RenameTable(
                name: "Itinerario_Actividad",
                newName: "ItinerarioActividades");

            migrationBuilder.RenameTable(
                name: "Itinerario",
                newName: "Itinerarios");

            migrationBuilder.RenameTable(
                name: "GuiaTuristico",
                newName: "GuiasTuristicos");

            migrationBuilder.RenameTable(
                name: "Guia_Paquete",
                newName: "GuiaPaquetes");

            migrationBuilder.RenameTable(
                name: "Grupo",
                newName: "Grupos");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias");

            migrationBuilder.RenameTable(
                name: "Atraccion",
                newName: "Atracciones");

            migrationBuilder.RenameTable(
                name: "Actividad_Atraccion",
                newName: "ActividadAtracciones");

            migrationBuilder.RenameTable(
                name: "Actividad",
                newName: "Actividades");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_ValoracionID",
                table: "Visitas",
                newName: "IX_Visitas_ValoracionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_ValidacionID",
                table: "Visitas",
                newName: "IX_Visitas_ValidacionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_TuristaID",
                table: "Visitas",
                newName: "IX_Visitas_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_ModificacionID",
                table: "Visitas",
                newName: "IX_Visitas_ModificacionID");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_ItinerarioID",
                table: "Visitas",
                newName: "IX_Visitas_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Visita_ActividadID",
                table: "Visitas",
                newName: "IX_Visitas_ActividadID");

            migrationBuilder.RenameIndex(
                name: "IX_Valoracion_TuristaID",
                table: "Valoraciones",
                newName: "IX_Valoraciones_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Valoracion_ItinerarioID",
                table: "Valoraciones",
                newName: "IX_Valoraciones_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Validacion_ItinerarioID",
                table: "Validaciones",
                newName: "IX_Validaciones_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Validacion_GuiaID",
                table: "Validaciones",
                newName: "IX_Validaciones_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Turista_Paquete_PaqueteID",
                table: "TuristaPaquetes",
                newName: "IX_TuristaPaquetes_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Turista_GrupoID",
                table: "Turistas",
                newName: "IX_Turistas_GrupoID");

            migrationBuilder.RenameIndex(
                name: "IX_Paquete_Categoria_CategoriaID",
                table: "PaqueteCategorias",
                newName: "IX_PaqueteCategorias_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificacion_TuristaID",
                table: "Modificaciones",
                newName: "IX_Modificaciones_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificacion_ItinerarioID",
                table: "Modificaciones",
                newName: "IX_Modificaciones_ItinerarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Modificacion_GuiaID",
                table: "Modificaciones",
                newName: "IX_Modificaciones_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Turista_TuristaID",
                table: "ItinerarioTuristas",
                newName: "IX_ItinerarioTuristas_TuristaID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_Actividad_ActividadID",
                table: "ItinerarioActividades",
                newName: "IX_ItinerarioActividades_ActividadID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_PaqueteID",
                table: "Itinerarios",
                newName: "IX_Itinerarios_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_GuiaID",
                table: "Itinerarios",
                newName: "IX_Itinerarios_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Guia_Paquete_PaqueteID",
                table: "GuiaPaquetes",
                newName: "IX_GuiaPaquetes_PaqueteID");

            migrationBuilder.RenameIndex(
                name: "IX_Atraccion_GuiaID",
                table: "Atracciones",
                newName: "IX_Atracciones_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Atraccion_CategoriaID",
                table: "Atracciones",
                newName: "IX_Atracciones_CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividad_Atraccion_AtraccionID",
                table: "ActividadAtracciones",
                newName: "IX_ActividadAtracciones_AtraccionID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividad_GuiaID",
                table: "Actividades",
                newName: "IX_Actividades_GuiaID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividad_CategoriaID",
                table: "Actividades",
                newName: "IX_Actividades_CategoriaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas",
                column: "VisitaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Valoraciones",
                table: "Valoraciones",
                column: "ValoracionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Validaciones",
                table: "Validaciones",
                column: "ValidacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TuristaPaquetes",
                table: "TuristaPaquetes",
                columns: new[] { "TuristaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turistas",
                table: "Turistas",
                column: "TuristaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaquetesTuristicos",
                table: "PaquetesTuristicos",
                column: "PaqueteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaqueteCategorias",
                table: "PaqueteCategorias",
                columns: new[] { "PaqueteID", "CategoriaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modificaciones",
                table: "Modificaciones",
                column: "ModificacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItinerarioTuristas",
                table: "ItinerarioTuristas",
                columns: new[] { "ItinerarioID", "TuristaID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItinerarioActividades",
                table: "ItinerarioActividades",
                columns: new[] { "ItinerarioID", "ActividadID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerarios",
                table: "Itinerarios",
                column: "ItinerarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuiasTuristicos",
                table: "GuiasTuristicos",
                column: "GuiaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuiaPaquetes",
                table: "GuiaPaquetes",
                columns: new[] { "GuiaID", "PaqueteID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grupos",
                table: "Grupos",
                column: "GrupoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "CategoriaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atracciones",
                table: "Atracciones",
                column: "AtraccionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActividadAtracciones",
                table: "ActividadAtracciones",
                columns: new[] { "ActividadID", "AtraccionID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actividades",
                table: "Actividades",
                column: "ActividadID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadAtracciones_Actividades_ActividadID",
                table: "ActividadAtracciones",
                column: "ActividadID",
                principalTable: "Actividades",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadAtracciones_Atracciones_AtraccionID",
                table: "ActividadAtracciones",
                column: "AtraccionID",
                principalTable: "Atracciones",
                principalColumn: "AtraccionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Categorias_CategoriaID",
                table: "Actividades",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_GuiasTuristicos_GuiaID",
                table: "Actividades",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atracciones_Categorias_CategoriaID",
                table: "Atracciones",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atracciones_GuiasTuristicos_GuiaID",
                table: "Atracciones",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuiaPaquetes_GuiasTuristicos_GuiaID",
                table: "GuiaPaquetes",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuiaPaquetes_PaquetesTuristicos_PaqueteID",
                table: "GuiaPaquetes",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItinerarioActividades_Actividades_ActividadID",
                table: "ItinerarioActividades",
                column: "ActividadID",
                principalTable: "Actividades",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItinerarioActividades_Itinerarios_ItinerarioID",
                table: "ItinerarioActividades",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_GuiasTuristicos_GuiaID",
                table: "Itinerarios",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_PaquetesTuristicos_PaqueteID",
                table: "Itinerarios",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItinerarioTuristas_Itinerarios_ItinerarioID",
                table: "ItinerarioTuristas",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItinerarioTuristas_Turistas_TuristaID",
                table: "ItinerarioTuristas",
                column: "TuristaID",
                principalTable: "Turistas",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_GuiasTuristicos_GuiaID",
                table: "Modificaciones",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Itinerarios_ItinerarioID",
                table: "Modificaciones",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modificaciones_Turistas_TuristaID",
                table: "Modificaciones",
                column: "TuristaID",
                principalTable: "Turistas",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaqueteCategorias_Categorias_CategoriaID",
                table: "PaqueteCategorias",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaqueteCategorias_PaquetesTuristicos_PaqueteID",
                table: "PaqueteCategorias",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TuristaPaquetes_PaquetesTuristicos_PaqueteID",
                table: "TuristaPaquetes",
                column: "PaqueteID",
                principalTable: "PaquetesTuristicos",
                principalColumn: "PaqueteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TuristaPaquetes_Turistas_TuristaID",
                table: "TuristaPaquetes",
                column: "TuristaID",
                principalTable: "Turistas",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turistas_Grupos_GrupoID",
                table: "Turistas",
                column: "GrupoID",
                principalTable: "Grupos",
                principalColumn: "GrupoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validaciones_GuiasTuristicos_GuiaID",
                table: "Validaciones",
                column: "GuiaID",
                principalTable: "GuiasTuristicos",
                principalColumn: "GuiaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validaciones_Itinerarios_ItinerarioID",
                table: "Validaciones",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoraciones_Itinerarios_ItinerarioID",
                table: "Valoraciones",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valoraciones_Turistas_TuristaID",
                table: "Valoraciones",
                column: "TuristaID",
                principalTable: "Turistas",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Actividades_ActividadID",
                table: "Visitas",
                column: "ActividadID",
                principalTable: "Actividades",
                principalColumn: "ActividadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Itinerarios_ItinerarioID",
                table: "Visitas",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Modificaciones_ModificacionID",
                table: "Visitas",
                column: "ModificacionID",
                principalTable: "Modificaciones",
                principalColumn: "ModificacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Turistas_TuristaID",
                table: "Visitas",
                column: "TuristaID",
                principalTable: "Turistas",
                principalColumn: "TuristaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Validaciones_ValidacionID",
                table: "Visitas",
                column: "ValidacionID",
                principalTable: "Validaciones",
                principalColumn: "ValidacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Valoraciones_ValoracionID",
                table: "Visitas",
                column: "ValoracionID",
                principalTable: "Valoraciones",
                principalColumn: "ValoracionID");
        }
    }
}
