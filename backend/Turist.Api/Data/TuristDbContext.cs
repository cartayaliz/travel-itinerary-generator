using Microsoft.EntityFrameworkCore;
using Turist.Api.Models;

namespace Turist.Api.Data;

public class TuristDbContext : DbContext
{
    public TuristDbContext(DbContextOptions<TuristDbContext> options) : base(options) { }

    
    public DbSet<GuiaTuristico> GuiasTuristicos { get; set; }
    public DbSet<PaqueteTuristico> PaquetesTuristicos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Turista> Turistas { get; set; }
    public DbSet<Itinerario> Itinerarios { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Autorizado> Autorizados { get; set; }
    public DbSet<Clasifica> Clasificaciones { get; set; }
    public DbSet<Inscribir> Inscripciones { get; set; }
    public DbSet<Asignar> Asignaciones { get; set; }
    public DbSet<Contiene> Contiene { get; set; }
    public DbSet<Validacion> Validaciones { get; set; }
    public DbSet<Modificacion> Modificaciones { get; set; }
    public DbSet<Valoracion> Valoraciones { get; set; }
    public DbSet<Visita> Visitas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Claves primarias de las entidades
    modelBuilder.Entity<GuiaTuristico>().HasKey(g => g.GuiaId);
    modelBuilder.Entity<PaqueteTuristico>().HasKey(p => p.PaqueteId);
    modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaId);
    modelBuilder.Entity<Grupo>().HasKey(g => g.GrupoId);
    modelBuilder.Entity<Turista>().HasKey(t => t.TuristaId);
    modelBuilder.Entity<Itinerario>().HasKey(i => i.ItinerarioId);
    modelBuilder.Entity<Actividad>().HasKey(a => a.ActividadId);
    modelBuilder.Entity<Validacion>().HasKey(v => v.ValidacionId);
    modelBuilder.Entity<Modificacion>().HasKey(m => m.ModificacionId);
    modelBuilder.Entity<Valoracion>().HasKey(v => v.ValoracionId);
    modelBuilder.Entity<Visita>().HasKey(v => v.VisitaId);

    // Claves primarias compuestas de las tablas intermedias
    modelBuilder.Entity<Autorizado>().HasKey(a => new { GuiaId = a.GuiaId, a.PaqueteId });
    modelBuilder.Entity<Clasifica>().HasKey(c => new { c.PaqueteId, c.CategoriaId });
    modelBuilder.Entity<Inscribir>().HasKey(i => new { i.TuristaId, i.PaqueteId });
    modelBuilder.Entity<Asignar>().HasKey(a => new { ItinerarioID = a.ItinerarioId, TuristaID = a.TuristaId });
    modelBuilder.Entity<Contiene>().HasKey(c => new { c.ItinerarioId, c.ActividadId });
}
}
