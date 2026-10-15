using Microsoft.EntityFrameworkCore;
using Turist.Api.Models;

namespace Turist.Api.Data;

public class TuristDbContext : DbContext
{
    public TuristDbContext(DbContextOptions<TuristDbContext> options) : base(options) { }

    
    public DbSet<GuiaTuristico> GuiaTuristico { get; set; }
    public DbSet<PaqueteTuristico> PaqueteTuristico { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Grupo> Grupo { get; set; }
    public DbSet<Turista> Turista { get; set; }
    public DbSet<Itinerario> Itinerario { get; set; }
    public DbSet<Actividad> Actividad { get; set; }
    public DbSet<Atraccion> Atraccion { get; set; }
    public DbSet<Guia_Paquete> Guia_Paquete { get; set; }
public DbSet<Paquete_Categoria> Paquete_Categoria { get; set; }
public DbSet<Turista_Paquete> Turista_Paquete { get; set; }
public DbSet<Itinerario_Turista> Itinerario_Turista { get; set; }
public DbSet<Itinerario_Actividad> Itinerario_Actividad { get; set; }
public DbSet<Actividad_Atraccion> Actividad_Atraccion { get; set; }
    public DbSet<Validacion> Validacion { get; set; }
    public DbSet<Modificacion> Modificacion { get; set; }
    public DbSet<Valoracion> Valoracion { get; set; }
    public DbSet<Visita> Visita { get; set; }

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
    modelBuilder.Entity<Atraccion>().HasKey(a => a.AtraccionID);

    // Claves primarias compuestas de las tablas intermedias
    modelBuilder.Entity<Guia_Paquete>().HasKey(gp => new { gp.GuiaID, gp.PaqueteID });
        modelBuilder.Entity<Paquete_Categoria>().HasKey(pc => new { pc.PaqueteID, pc.CategoriaID });
        modelBuilder.Entity<Turista_Paquete>().HasKey(tp => new { tp.TuristaID, tp.PaqueteID });
        modelBuilder.Entity<Itinerario_Turista>().HasKey(it => new { it.ItinerarioID, it.TuristaID });
        modelBuilder.Entity<Itinerario_Actividad>().HasKey(ia => new { ia.ItinerarioID, ia.ActividadID });
        modelBuilder.Entity<Actividad_Atraccion>().HasKey(aa => new { aa.ActividadID, aa.AtraccionID }); 
}
}
