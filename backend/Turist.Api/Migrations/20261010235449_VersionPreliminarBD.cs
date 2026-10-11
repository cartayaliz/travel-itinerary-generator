using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Turist.Api.Migrations
{
    /// <inheritdoc />
    public partial class VersionPreliminarBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atracciones");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    GrupoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.GrupoID);
                });

            migrationBuilder.CreateTable(
                name: "GuiasTuristicos",
                columns: table => new
                {
                    GuiaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Especialidad = table.Column<string>(type: "text", nullable: false),
                    EsJefe = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiasTuristicos", x => x.GuiaID);
                });

            migrationBuilder.CreateTable(
                name: "PaquetesTuristicos",
                columns: table => new
                {
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Destino = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaquetesTuristicos", x => x.PaqueteID);
                });

            migrationBuilder.CreateTable(
                name: "Turistas",
                columns: table => new
                {
                    TuristaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GrupoID = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turistas", x => x.TuristaID);
                    table.ForeignKey(
                        name: "FK_Turistas_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    ActividadID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    NivelEsfuerzo = table.Column<string>(type: "text", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.ActividadID);
                    table.ForeignKey(
                        name: "FK_Actividades_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividades_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
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
                name: "Itinerarios",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerarios", x => x.ItinerarioID);
                    table.ForeignKey(
                        name: "FK_Itinerarios_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itinerarios_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
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
                name: "Modificaciones",
                columns: table => new
                {
                    ModificacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modificaciones", x => x.ModificacionID);
                    table.ForeignKey(
                        name: "FK_Modificaciones_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID");
                    table.ForeignKey(
                        name: "FK_Modificaciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modificaciones_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validaciones",
                columns: table => new
                {
                    ValidacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaValidacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validaciones", x => x.ValidacionID);
                    table.ForeignKey(
                        name: "FK_Validaciones_GuiasTuristicos_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiasTuristicos",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Validaciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoraciones",
                columns: table => new
                {
                    ValoracionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoraciones", x => x.ValoracionID);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    VisitaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidacionID = table.Column<int>(type: "integer", nullable: true),
                    ModificacionID = table.Column<int>(type: "integer", nullable: true),
                    ValoracionID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.VisitaID);
                    table.ForeignKey(
                        name: "FK_Visitas_Actividades_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Modificaciones_ModificacionID",
                        column: x => x.ModificacionID,
                        principalTable: "Modificaciones",
                        principalColumn: "ModificacionID");
                    table.ForeignKey(
                        name: "FK_Visitas_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Validaciones_ValidacionID",
                        column: x => x.ValidacionID,
                        principalTable: "Validaciones",
                        principalColumn: "ValidacionID");
                    table.ForeignKey(
                        name: "FK_Visitas_Valoraciones_ValoracionID",
                        column: x => x.ValoracionID,
                        principalTable: "Valoraciones",
                        principalColumn: "ValoracionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_CategoriaID",
                table: "Actividades",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_GuiaID",
                table: "Actividades",
                column: "GuiaID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_GuiaID",
                table: "Itinerarios",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_PaqueteID",
                table: "Itinerarios",
                column: "PaqueteID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificaciones_GuiaID",
                table: "Modificaciones",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificaciones_ItinerarioID",
                table: "Modificaciones",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificaciones_TuristaID",
                table: "Modificaciones",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Turistas_GrupoID",
                table: "Turistas",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Validaciones_GuiaID",
                table: "Validaciones",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Validaciones_ItinerarioID",
                table: "Validaciones",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_ItinerarioID",
                table: "Valoraciones",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_TuristaID",
                table: "Valoraciones",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ActividadID",
                table: "Visitas",
                column: "ActividadID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ItinerarioID",
                table: "Visitas",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ModificacionID",
                table: "Visitas",
                column: "ModificacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_TuristaID",
                table: "Visitas",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ValidacionID",
                table: "Visitas",
                column: "ValidacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ValoracionID",
                table: "Visitas",
                column: "ValoracionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Modificaciones");

            migrationBuilder.DropTable(
                name: "Validaciones");

            migrationBuilder.DropTable(
                name: "Valoraciones");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Itinerarios");

            migrationBuilder.DropTable(
                name: "Turistas");

            migrationBuilder.DropTable(
                name: "GuiasTuristicos");

            migrationBuilder.DropTable(
                name: "PaquetesTuristicos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.CreateTable(
                name: "Atracciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Categoria = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    NivelEsfuerzo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atracciones", x => x.Id);
                });
        }
    }
}
