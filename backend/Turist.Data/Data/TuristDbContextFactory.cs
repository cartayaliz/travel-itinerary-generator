using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Turist.Data;

public class TuristDbContextFactory : IDesignTimeDbContextFactory<TuristDbContext>
{
    public TuristDbContext CreateDbContext(string[] args)
    {
        // Aquí leemos la cadena de conexión. 
        // Puedes poner la misma que tienes en appsettings.Development.json
        var connectionString = "Host=localhost;Port=5432;Database=turismo_db;Username=turismo;Password=turismo123";

        var optionsBuilder = new DbContextOptionsBuilder<TuristDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TuristDbContext(optionsBuilder.Options);
    }
}