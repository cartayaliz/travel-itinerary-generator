using Microsoft.EntityFrameworkCore;
using Turist.Domain.Models;
using Turist.Domain.Repository;
using Turist.Data;

namespace Turist.Api.Infrastructure;

public class Repository : IRepository<Usuario>
{
    private readonly TuristDbContext _context;

    public Repository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync() => 
        await _context.Set<Usuario>().ToListAsync();

    public async Task<Usuario?> GetByIdAsync(int id) => 
        await _context.Set<Usuario>().FindAsync(id);

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Set<Usuario>().AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Set<Usuario>().Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        _context.Set<Usuario>().Remove(usuario);
        await _context.SaveChangesAsync();
    }
}