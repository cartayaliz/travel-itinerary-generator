using Microsoft.EntityFrameworkCore;
using Turist.Api.Models;

namespace Turist.Api.Data;

public class TuristDbContext : DbContext
{
    public TuristDbContext(DbContextOptions<TuristDbContext> options) : base(options) { }

    public DbSet<Atraccion> Atracciones { get; set; }
}
