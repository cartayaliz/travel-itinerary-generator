using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class ValidacionRepository : IValidacionRepository
{
    private readonly TuristDbContext _context;

    public ValidacionRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Validacion>> GetAllAsync()
    {
        return await _context.Validaciones.ToListAsync();
    }

    public async Task<Validacion?> GetByIdAsync(int id)
    {
        return await _context.Validaciones.FindAsync(id);
    }

    public async Task AddAsync(Validacion validacion)
    {
        await _context.Validaciones.AddAsync(validacion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Validacion validacion)
    {
        _context.Validaciones.Update(validacion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Validacion validacion)
    {
        _context.Validaciones.Remove(validacion);
        await _context.SaveChangesAsync();
    }
}