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

    public async Task<IEnumerable<Itinerario>> GetAllAsync()
    {
        return await _context.Itinerarios.ToListAsync();
    }

    public async Task<Itinerario?> GetByIdAsync(int id)
    {
        return await _context.Itinerarios.FindAsync(id);
    }

    public async Task AddAsync(Itinerario itinerario)
    {
        await _context.Itinerarios.AddAsync(itinerario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Itinerario itinerario)
    {
        _context.Itinerarios.Update(itinerario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Itinerario itinerario)
    {
        _context.Itinerarios.Remove(itinerario);
        await _context.SaveChangesAsync();
    }

    
    public async Task LimpiarParadasAsync(int itinerarioId)
    {
        var paradasExistentes = await _context.Itinerario_Paradas
            .Where(ip => ip.ItinerarioID == itinerarioId)
            .ToListAsync();

        if (paradasExistentes.Any())
        {
            _context.Itinerario_Paradas.RemoveRange(paradasExistentes);
            await _context.SaveChangesAsync();
        }
    }

    
    public async Task AsignarParadasAsync(List<Itinerario_Parada> paradas)
    {
        await _context.Itinerario_Paradas.AddRangeAsync(paradas);
        await _context.SaveChangesAsync();
    }
}