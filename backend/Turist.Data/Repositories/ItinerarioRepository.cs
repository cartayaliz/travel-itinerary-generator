using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class ItinerarioRepository : IItinerarioRepository
{
    private readonly TuristDbContext _context;

    public ItinerarioRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Itinerario>> GetAllWithParadasOrderedAsync()
    {
        var itinerarios = await _context.Itinerarios
            .Include(i => i.ItinerarioParadas)
            .ThenInclude(ip => ip.Parada)
            .ToListAsync();

        // Ordenamos las paradas por su orden dentro de cada itinerario
        foreach (var itinerario in itinerarios)
        {
            itinerario.ItinerarioParadas = [.. itinerario.ItinerarioParadas.OrderBy(ip => ip.Orden)];
        }

        return itinerarios;
    }
}
