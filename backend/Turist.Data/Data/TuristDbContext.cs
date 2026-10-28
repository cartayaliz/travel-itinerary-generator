using Microsoft.EntityFrameworkCore;
using Turist.Domain.Models;

namespace Turist.Data;

public class TuristDbContext : DbContext
{
    public TuristDbContext(DbContextOptions<TuristDbContext> options) : base(options) { }

    // DbSets 
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<JefeOperaciones> JefesOperaciones { get; set; }
    public DbSet<GuiaTuristico> GuiasTuristicos { get; set; }
    public DbSet<Turista> Turistas { get; set; }
    public DbSet<PaqueteTuristico> PaquetesTuristicos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Itinerario> Itinerarios { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Atraccion> Atracciones { get; set; }
    public DbSet<Parada> Paradas { get; set; }
    public DbSet<Validacion> Validaciones { get; set; }
    public DbSet<Modificacion> Modificaciones { get; set; }
    public DbSet<Valoracion> Valoraciones { get; set; }
    public DbSet<Visita> Visitas { get; set; }

    // Tablas Intermedias
    public DbSet<Guia_Paquete> Guia_Paquetes { get; set; }
    public DbSet<Paquete_Categoria> Paquete_Categorias { get; set; }
    public DbSet<Turista_Paquete> Turista_Paquetes { get; set; }
    public DbSet<Itinerario_Turista> Itinerario_Turistas { get; set; }
    public DbSet<Itinerario_Paquete> Itinerario_Paquetes { get; set; }
    public DbSet<Itinerario_Parada> Itinerario_Paradas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Claves primarias
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

        // Claves primarias compuestas (Tablas intermedias)
        modelBuilder.Entity<Guia_Paquete>().HasKey(gp => new { gp.GuiaID, gp.PaqueteID });
        modelBuilder.Entity<Paquete_Categoria>().HasKey(pc => new { pc.PaqueteID, pc.CategoriaID });
        modelBuilder.Entity<Turista_Paquete>().HasKey(tp => new { tp.TuristaID, tp.PaqueteID });
        modelBuilder.Entity<Itinerario_Turista>().HasKey(it => new { it.ItinerarioID, it.TuristaID });
        modelBuilder.Entity<Itinerario_Paquete>().HasKey(ip => new { ip.ItinerarioID, ip.PaqueteID });
        modelBuilder.Entity<Itinerario_Parada>().HasKey(ip => new { ip.ItinerarioID, ip.ParadaID });


        // Convertir Enums a String en la Base de Datos
        modelBuilder.Entity<Actividad>().Property(a => a.Tipo).HasConversion<string>();
        modelBuilder.Entity<Actividad>().Property(a => a.NivelEsfuerzo).HasConversion<string>();
        modelBuilder.Entity<Itinerario>().Property(i => i.Origen).HasConversion<string>();
        modelBuilder.Entity<Visita>().Property(v => v.Estado).HasConversion<string>();
        modelBuilder.Entity<Validacion>().Property(v => v.Estado).HasConversion<string>();
        modelBuilder.Entity<Modificacion>().Property(m => m.Estado).HasConversion<string>();
    }
}