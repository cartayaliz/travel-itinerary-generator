using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class GrupoRepository : IGrupoRepository
{
    private readonly TuristDbContext _context;

    public GrupoRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Grupo>> GetAllAsync()
    {
        return await _context.Grupos.ToListAsync();
    }

    public async Task<Grupo?> GetByIdAsync(int id)
    {
        return await _context.Grupos.FindAsync(id);
    }

    public async Task AddAsync(Grupo grupo)
    {
        await _context.Grupos.AddAsync(grupo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Grupo grupo)
    {
        _context.Grupos.Update(grupo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Grupo grupo)
    {
        _context.Grupos.Remove(grupo);
        await _context.SaveChangesAsync();
    }
}