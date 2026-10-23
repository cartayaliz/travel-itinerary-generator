using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Turist.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworknewMerx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Actividad_ActividadID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Atraccion_AtraccionID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_GuiaTuristico_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_PaqueteTuristico_PaqueteID",
                table: "Guia_Paquete");

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
                name: "FK_Paquete_Categoria_Categoria_CategoriaID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_PaqueteTuristico_PaqueteID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_PaqueteTuristico_PaqueteID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_Turista_TuristaID",
                table: "Turista_Paquete");

            migrationBuilder.DropTable(
                name: "Atraccion");

            migrationBuilder.DropTable(
                name: "Visita");

            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Modificacion");

            migrationBuilder.DropTable(
                name: "Validacion");

            migrationBuilder.DropTable(
                name: "Valoracion");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Itinerario");

            migrationBuilder.DropTable(
                name: "Turista");

            migrationBuilder.DropTable(
                name: "GuiaTuristico");

            migrationBuilder.DropTable(
                name: "PaqueteTuristico");

            migrationBuilder.DropTable(
                name: "Grupo");

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
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Especialidad = table.Column<string>(type: "text", nullable: true),
                    Edad = table.Column<int>(type: "integer", nullable: true),
                    GrupoID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuario_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerarios",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerarios", x => x.ItinerarioID);
                    table.ForeignKey(
                        name: "FK_Itinerarios_Usuario_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paradas",
                columns: table => new
                {
                    ParadaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    NivelEsfuerzo = table.Column<string>(type: "text", nullable: true),
                    HoraInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    DuracionMinutos = table.Column<int>(type: "integer", nullable: true),
                    HorarioApertura = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    HorarioCierre = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paradas", x => x.ParadaID);
                    table.ForeignKey(
                        name: "FK_Paradas_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paradas_Usuario_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerario_Paquetes",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario_Paquetes", x => new { x.ItinerarioID, x.PaqueteID });
                    table.ForeignKey(
                        name: "FK_Itinerario_Paquetes_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itinerario_Paquetes_PaquetesTuristicos_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modificaciones",
                columns: table => new
                {
                    ModificacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modificaciones", x => x.ModificacionID);
                    table.ForeignKey(
                        name: "FK_Modificaciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modificaciones_Usuario_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID");
                    table.ForeignKey(
                        name: "FK_Modificaciones_Usuario_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validaciones",
                columns: table => new
                {
                    ValidacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaValidacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    JefeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validaciones", x => x.ValidacionID);
                    table.ForeignKey(
                        name: "FK_Validaciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Validaciones_Usuario_JefeID",
                        column: x => x.JefeID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerario_Parada",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    ParadaID = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario_Parada", x => new { x.ItinerarioID, x.ParadaID });
                    table.ForeignKey(
                        name: "FK_Itinerario_Parada_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itinerario_Parada_Paradas_ParadaID",
                        column: x => x.ParadaID,
                        principalTable: "Paradas",
                        principalColumn: "ParadaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    VisitaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    ParadaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.VisitaID);
                    table.ForeignKey(
                        name: "FK_Visitas_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Paradas_ParadaID",
                        column: x => x.ParadaID,
                        principalTable: "Paradas",
                        principalColumn: "ParadaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Usuario_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoraciones",
                columns: table => new
                {
                    ValoracionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: true),
                    VisitaID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoraciones", x => x.ValoracionID);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Itinerarios_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerarios",
                        principalColumn: "ItinerarioID");
                    table.ForeignKey(
                        name: "FK_Valoraciones_Usuario_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Visitas_VisitaID",
                        column: x => x.VisitaID,
                        principalTable: "Visitas",
                        principalColumn: "VisitaID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_Paquetes_PaqueteID",
                table: "Itinerario_Paquetes",
                column: "PaqueteID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_Parada_ParadaID",
                table: "Itinerario_Parada",
                column: "ParadaID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_GuiaID",
                table: "Itinerarios",
                column: "GuiaID");

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
                name: "IX_Paradas_CategoriaID",
                table: "Paradas",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Paradas_GuiaID",
                table: "Paradas",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GrupoID",
                table: "Usuario",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Validaciones_ItinerarioID",
                table: "Validaciones",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Validaciones_JefeID",
                table: "Validaciones",
                column: "JefeID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_ItinerarioID",
                table: "Valoraciones",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_TuristaID",
                table: "Valoraciones",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_VisitaID",
                table: "Valoraciones",
                column: "VisitaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ItinerarioID",
                table: "Visitas",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_ParadaID",
                table: "Visitas",
                column: "ParadaID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_TuristaID",
                table: "Visitas",
                column: "TuristaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Atraccion_Paradas_ActividadID",
                table: "Actividad_Atraccion",
                column: "ActividadID",
                principalTable: "Paradas",
                principalColumn: "ParadaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Atraccion_Paradas_AtraccionID",
                table: "Actividad_Atraccion",
                column: "AtraccionID",
                principalTable: "Paradas",
                principalColumn: "ParadaID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Itinerario_Actividad_Itinerarios_ItinerarioID",
                table: "Itinerario_Actividad",
                column: "ItinerarioID",
                principalTable: "Itinerarios",
                principalColumn: "ItinerarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_Actividad_Paradas_ActividadID",
                table: "Itinerario_Actividad",
                column: "ActividadID",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Paradas_ActividadID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Atraccion_Paradas_AtraccionID",
                table: "Actividad_Atraccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Guia_Paquete_Usuario_GuiaID",
                table: "Guia_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Actividad_Itinerarios_ItinerarioID",
                table: "Itinerario_Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Actividad_Paradas_ActividadID",
                table: "Itinerario_Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Itinerarios_ItinerarioID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_Turista_Usuario_TuristaID",
                table: "Itinerario_Turista");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_Categorias_CategoriaID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Paquete_Categoria_PaquetesTuristicos_PaqueteID",
                table: "Paquete_Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_PaquetesTuristicos_PaqueteID",
                table: "Turista_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Turista_Paquete_Usuario_TuristaID",
                table: "Turista_Paquete");

            migrationBuilder.DropTable(
                name: "Itinerario_Paquetes");

            migrationBuilder.DropTable(
                name: "Itinerario_Parada");

            migrationBuilder.DropTable(
                name: "Modificaciones");

            migrationBuilder.DropTable(
                name: "Validaciones");

            migrationBuilder.DropTable(
                name: "Valoraciones");

            migrationBuilder.DropTable(
                name: "PaquetesTuristicos");

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropTable(
                name: "Itinerarios");

            migrationBuilder.DropTable(
                name: "Paradas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    GrupoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.GrupoID);
                });

            migrationBuilder.CreateTable(
                name: "GuiaTuristico",
                columns: table => new
                {
                    GuiaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EsJefe = table.Column<bool>(type: "boolean", nullable: false),
                    Especialidad = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaTuristico", x => x.GuiaID);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteTuristico",
                columns: table => new
                {
                    PaqueteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Destino = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteTuristico", x => x.PaqueteID);
                });

            migrationBuilder.CreateTable(
                name: "Turista",
                columns: table => new
                {
                    TuristaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GrupoID = table.Column<int>(type: "integer", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turista", x => x.TuristaID);
                    table.ForeignKey(
                        name: "FK_Turista_Grupo_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupo",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    ActividadID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    NivelEsfuerzo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.ActividadID);
                    table.ForeignKey(
                        name: "FK_Actividad_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividad_GuiaTuristico_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiaTuristico",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atraccion",
                columns: table => new
                {
                    AtraccionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaID = table.Column<int>(type: "integer", nullable: false),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atraccion", x => x.AtraccionID);
                    table.ForeignKey(
                        name: "FK_Atraccion_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atraccion_GuiaTuristico_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiaTuristico",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerario",
                columns: table => new
                {
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    PaqueteID = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario", x => x.ItinerarioID);
                    table.ForeignKey(
                        name: "FK_Itinerario_GuiaTuristico_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiaTuristico",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itinerario_PaqueteTuristico_PaqueteID",
                        column: x => x.PaqueteID,
                        principalTable: "PaqueteTuristico",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modificacion",
                columns: table => new
                {
                    ModificacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuiaID = table.Column<int>(type: "integer", nullable: true),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modificacion", x => x.ModificacionID);
                    table.ForeignKey(
                        name: "FK_Modificacion_GuiaTuristico_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiaTuristico",
                        principalColumn: "GuiaID");
                    table.ForeignKey(
                        name: "FK_Modificacion_Itinerario_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerario",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modificacion_Turista_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turista",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validacion",
                columns: table => new
                {
                    ValidacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuiaID = table.Column<int>(type: "integer", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaValidacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validacion", x => x.ValidacionID);
                    table.ForeignKey(
                        name: "FK_Validacion_GuiaTuristico_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "GuiaTuristico",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Validacion_Itinerario_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerario",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoracion",
                columns: table => new
                {
                    ValoracionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoracion", x => x.ValoracionID);
                    table.ForeignKey(
                        name: "FK_Valoracion_Itinerario_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerario",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valoracion_Turista_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turista",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    VisitaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActividadID = table.Column<int>(type: "integer", nullable: false),
                    ItinerarioID = table.Column<int>(type: "integer", nullable: false),
                    ModificacionID = table.Column<int>(type: "integer", nullable: true),
                    TuristaID = table.Column<int>(type: "integer", nullable: false),
                    ValidacionID = table.Column<int>(type: "integer", nullable: true),
                    ValoracionID = table.Column<int>(type: "integer", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.VisitaID);
                    table.ForeignKey(
                        name: "FK_Visita_Actividad_ActividadID",
                        column: x => x.ActividadID,
                        principalTable: "Actividad",
                        principalColumn: "ActividadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Itinerario_ItinerarioID",
                        column: x => x.ItinerarioID,
                        principalTable: "Itinerario",
                        principalColumn: "ItinerarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Modificacion_ModificacionID",
                        column: x => x.ModificacionID,
                        principalTable: "Modificacion",
                        principalColumn: "ModificacionID");
                    table.ForeignKey(
                        name: "FK_Visita_Turista_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turista",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Validacion_ValidacionID",
                        column: x => x.ValidacionID,
                        principalTable: "Validacion",
                        principalColumn: "ValidacionID");
                    table.ForeignKey(
                        name: "FK_Visita_Valoracion_ValoracionID",
                        column: x => x.ValoracionID,
                        principalTable: "Valoracion",
                        principalColumn: "ValoracionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_CategoriaID",
                table: "Actividad",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_GuiaID",
                table: "Actividad",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Atraccion_CategoriaID",
                table: "Atraccion",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Atraccion_GuiaID",
                table: "Atraccion",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_GuiaID",
                table: "Itinerario",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_PaqueteID",
                table: "Itinerario",
                column: "PaqueteID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificacion_GuiaID",
                table: "Modificacion",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificacion_ItinerarioID",
                table: "Modificacion",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Modificacion_TuristaID",
                table: "Modificacion",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Turista_GrupoID",
                table: "Turista",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Validacion_GuiaID",
                table: "Validacion",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Validacion_ItinerarioID",
                table: "Validacion",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoracion_ItinerarioID",
                table: "Valoracion",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoracion_TuristaID",
                table: "Valoracion",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ActividadID",
                table: "Visita",
                column: "ActividadID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ItinerarioID",
                table: "Visita",
                column: "ItinerarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ModificacionID",
                table: "Visita",
                column: "ModificacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_TuristaID",
                table: "Visita",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ValidacionID",
                table: "Visita",
                column: "ValidacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ValoracionID",
                table: "Visita",
                column: "ValoracionID");

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
        }
    }
}
