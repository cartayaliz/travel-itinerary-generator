using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class VisitaRepository : IVisitaRepository
{
    private readonly TuristDbContext _context;

    public VisitaRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Visita>> GetAllAsync()
    {
        return await _context.Visitas.ToListAsync();
    }

    public async Task<Visita?> GetByIdAsync(int id)
    {
        return await _context.Visitas.FindAsync(id);
    }

    public async Task AddAsync(Visita visita)
    {
        await _context.Visitas.AddAsync(visita);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Visita visita)
    {
        _context.Visitas.Update(visita);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Visita visita)
    {
        _context.Visitas.Remove(visita);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteVisitaAsync(int itinerarioId, int turistaId, int paradaId)
    {
        return await _context.Visitas
            .AnyAsync(v => v.ItinerarioID == itinerarioId
                        && v.TuristaID == turistaId
                        && v.ParadaID == paradaId);
    }
}