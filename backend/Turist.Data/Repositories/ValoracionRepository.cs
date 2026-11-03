using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class ValoracionRepository : IValoracionRepository
{
    private readonly TuristDbContext _context;

    public ValoracionRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Valoracion>> GetAllAsync()
    {
        return await _context.Valoraciones.ToListAsync();
    }

    public async Task<Valoracion?> GetByIdAsync(int id)
    {
        return await _context.Valoraciones.FindAsync(id);
    }

    public async Task AddAsync(Valoracion valoracion)
    {
        await _context.Valoraciones.AddAsync(valoracion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Valoracion valoracion)
    {
        _context.Valoraciones.Update(valoracion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Valoracion valoracion)
    {
        _context.Valoraciones.Remove(valoracion);
        await _context.SaveChangesAsync();
    }
}