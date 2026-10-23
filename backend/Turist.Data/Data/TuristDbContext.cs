using Microsoft.EntityFrameworkCore;
using Turist.Domain.Models;

namespace Turist.Data;

public class TuristDbContext : DbContext
{
    public TuristDbContext(DbContextOptions<TuristDbContext> options) : base(options) { }

    public DbSet<JefeOperaciones> JefesOperaciones { get; set; }
    public DbSet<GuiaTuristico> GuiasTuristicos { get; set; }
    public DbSet<PaqueteTuristico> PaquetesTuristicos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Turista> Turistas { get; set; }
    public DbSet<Itinerario> Itinerarios { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Atraccion> Atracciones { get; set; }
    public DbSet<Guia_Paquete> Guia_Paquete { get; set; }
    public DbSet<Paquete_Categoria> Paquete_Categoria { get; set; }
    public DbSet<Turista_Paquete> Turista_Paquete { get; set; }
    public DbSet<Itinerario_Turista> Itinerario_Turista { get; set; }
    public DbSet<Itinerario_Actividad> Itinerario_Actividad { get; set; }
    public DbSet<Actividad_Atraccion> Actividad_Atraccion { get; set; }
    public DbSet<Validacion> Validaciones { get; set; }
    public DbSet<Modificacion> Modificaciones { get; set; }
    public DbSet<Valoracion> Valoraciones { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<Parada> Paradas { get; set; }
    public DbSet<Itinerario_Paquete> Itinerario_Paquetes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Claves primarias de las entidades
        modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioID);
        modelBuilder.Entity<PaqueteTuristico>().HasKey(p => p.PaqueteID);
        modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaID);
        modelBuilder.Entity<Grupo>().HasKey(g => g.GrupoID);
        modelBuilder.Entity<Itinerario>().HasKey(i => i.ItinerarioID);
        modelBuilder.Entity<Validacion>().HasKey(v => v.ValidacionID);
        modelBuilder.Entity<Modificacion>().HasKey(m => m.ModificacionID);
        modelBuilder.Entity<Valoracion>().HasKey(v => v.ValoracionID);
        modelBuilder.Entity<Visita>().HasKey(v => v.VisitaID);
        modelBuilder.Entity<Parada>().HasKey(p => p.ParadaID);

        // Claves primarias compuestas de las tablas intermedias
        modelBuilder.Entity<Guia_Paquete>().HasKey(gp => new { gp.GuiaID, gp.PaqueteID });
        modelBuilder.Entity<Paquete_Categoria>().HasKey(pc => new { pc.PaqueteID, pc.CategoriaID });
        modelBuilder.Entity<Turista_Paquete>().HasKey(tp => new { tp.TuristaID, tp.PaqueteID });
        modelBuilder.Entity<Itinerario_Turista>().HasKey(it => new { it.ItinerarioID, it.TuristaID });
        modelBuilder.Entity<Itinerario_Actividad>().HasKey(ia => new { ia.ItinerarioID, ia.ActividadID });
        modelBuilder.Entity<Actividad_Atraccion>().HasKey(aa => new { aa.ActividadID, aa.AtraccionID });
        modelBuilder.Entity<Itinerario_Paquete>().HasKey(ip => new { ip.ItinerarioID, ip.PaqueteID });
        modelBuilder.Entity<Itinerario_Parada>().HasKey(ip => new { ip.ItinerarioID, ip.ParadaID });
    }
}
