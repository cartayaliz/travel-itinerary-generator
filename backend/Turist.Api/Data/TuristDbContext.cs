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
    modelBuilder.Entity<GuiaTuristico>().HasKey(g => g.GuiaID);
    modelBuilder.Entity<PaqueteTuristico>().HasKey(p => p.PaqueteID);
    modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaID);
    modelBuilder.Entity<Grupo>().HasKey(g => g.GrupoID);
    modelBuilder.Entity<Turista>().HasKey(t => t.TuristaID);
    modelBuilder.Entity<Itinerario>().HasKey(i => i.ItinerarioID);
    modelBuilder.Entity<Actividad>().HasKey(a => a.ActividadID);
    modelBuilder.Entity<Validacion>().HasKey(v => v.ValidacionID);
    modelBuilder.Entity<Modificacion>().HasKey(m => m.ModificacionID);
    modelBuilder.Entity<Valoracion>().HasKey(v => v.ValoracionID);
    modelBuilder.Entity<Visita>().HasKey(v => v.VisitaID);

    // Claves primarias compuestas de las tablas intermedias
    modelBuilder.Entity<Autorizado>().HasKey(a => new { a.GuiaID, a.PaqueteID });
    modelBuilder.Entity<Clasifica>().HasKey(c => new { c.PaqueteID, c.CategoriaID });
    modelBuilder.Entity<Inscribir>().HasKey(i => new { i.TuristaID, i.PaqueteID });
    modelBuilder.Entity<Asignar>().HasKey(a => new { a.ItinerarioID, a.TuristaID });
    modelBuilder.Entity<Contiene>().HasKey(c => new { c.ItinerarioID, c.ActividadID });
}
}
