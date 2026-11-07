using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class ModificacionRepository : IModificacionRepository
{
    private readonly TuristDbContext _context;

    public ModificacionRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Modificacion>> GetAllAsync()
    {
        return await _context.Modificaciones.ToListAsync();
    }

    public async Task<Modificacion?> GetByIdAsync(int id)
    {
        return await _context.Modificaciones.FindAsync(id);
    }

    public async Task AddAsync(Modificacion modificacion)
    {
        await _context.Modificaciones.AddAsync(modificacion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Modificacion modificacion)
    {
        _context.Modificaciones.Update(modificacion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Modificacion modificacion)
    {
        _context.Modificaciones.Remove(modificacion);
        await _context.SaveChangesAsync();
    }
}