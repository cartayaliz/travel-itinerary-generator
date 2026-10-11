using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Turist.Api.Migrations
{
    /// <inheritdoc />
    public partial class Rework_Atraccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones");

            migrationBuilder.DropTable(
                name: "Autorizados");

            migrationBuilder.DropTable(
                name: "Clasificaciones");

            migrationBuilder.DropTable(
                name: "Contiene");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.CreateTable(
                name: "Atracciones",
                columns: table => new
                {
                    AtraccionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atracciones", x => x.AtraccionID);
                    table.ForeignKey(
                        name: "FK_Atracciones_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atracciones_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuiaPaquetes",
                columns: table => new
                {
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaPaquetes", x => new { x.GuiaID, x.PaqueteID });
                    table.ForeignKey(
                        name: "FK_GuiaPaquetes_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuiaPaquetes_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItinerarioActividades",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItinerarioActividades", x => new { x.ItinerarioID, x.ActividadID });
                    table.ForeignKey(
                        name: "FK_ItinerarioActividades_Actividades_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItinerarioActividades_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItinerarioTuristas",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItinerarioTuristas", x => new { x.ItinerarioID, x.TuristaID });
                    table.ForeignKey(
                        name: "FK_ItinerarioTuristas_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItinerarioTuristas_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteCategorias",
                columns: table => new
                {
                    PaqueteID = table.Column<int>(type: "integer", nullable: false),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteCategorias", x => new { x.PaqueteID, x.CategoriaID });
                    table.ForeignKey(
                        name: "FK_PaqueteCategorias_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteCategorias_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TuristaPaquetes",
                columns: table => new
                {
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuristaPaquetes", x => new { x.TuristaID, x.PaqueteID });
                    table.ForeignKey(
                        name: "FK_TuristaPaquetes_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TuristaPaquetes_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActividadAtracciones",
                columns: table => new
                {
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    AtraccionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadAtracciones", x => new { x.ActividadID, x.AtraccionID });
                    table.ForeignKey(
                        name: "FK_ActividadAtracciones_Actividades_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadAtracciones_Atracciones_AtraccionID",
                        column: x => x.AtraccionID,
                        principalTable: "Atracciones",
                        principalColumn: "AtraccionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadAtracciones_AtraccionID",
                table: "ActividadAtracciones",
                column: "AtraccionID");

            migrationBuilder.CreateIndex(
                name: "IX_Atracciones_CategoriaID",
                table: "Atracciones",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Atracciones_GuiaID",
                table: "Atracciones",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_GuiaPaquetes_PaqueteID",
                table: "GuiaPaquetes",
                column: "PaqueteID");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioActividades_ActividadID",
                table: "ItinerarioActividades",
                column: "ActividadID");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioTuristas_TuristaID",
                table: "ItinerarioTuristas",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteCategorias_CategoriaID",
                table: "PaqueteCategorias",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_TuristaPaquetes_PaqueteID",
                table: "TuristaPaquetes",
                column: "PaqueteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadAtracciones");

            migrationBuilder.DropTable(
                name: "GuiaPaquetes");

            migrationBuilder.DropTable(
                name: "ItinerarioActividades");

            migrationBuilder.DropTable(
                name: "ItinerarioTuristas");

            migrationBuilder.DropTable(
                name: "PaqueteCategorias");

            migrationBuilder.DropTable(
                name: "TuristaPaquetes");

            migrationBuilder.DropTable(
                name: "Atracciones");

            migrationBuilder.CreateTable(
                name: "Asignaciones",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones", x => new { x.ItinerarioID, x.TuristaID });
                    table.ForeignKey(
                        name: "FK_Asignaciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Autorizados",
                columns: table => new
                {
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizados", x => new { x.GuiaID, x.PaqueteID });
                    table.ForeignKey(
                        name: "FK_Autorizados_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Autorizados_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clasificaciones",
                columns: table => new
                {
                    PaqueteID = table.Column<int>(type: "integer", nullable: false),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasificaciones", x => new { x.PaqueteID, x.CategoriaID });
                    table.ForeignKey(
                        name: "FK_Clasificaciones_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clasificaciones_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contiene",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contiene", x => new { x.ItinerarioID, x.ActividadID });
                    table.ForeignKey(
                        name: "FK_Contiene_Actividades_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contiene_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => new { x.TuristaID, x.PaqueteID });
                    table.ForeignKey(
                        name: "FK_Inscripciones_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_TuristaID",
                table: "Asignaciones",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Autorizados_PaqueteID",
                table: "Autorizados",
                column: "PaqueteID");

            migrationBuilder.CreateIndex(
                name: "IX_Clasificaciones_CategoriaID",
                table: "Clasificaciones",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Contiene_ActividadID",
                table: "Contiene",
                column: "ActividadID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_PaqueteID",
                table: "Inscripciones",
                column: "PaqueteID");
        }
    }
}
