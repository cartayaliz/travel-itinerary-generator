using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class PaqueteTuristicoRepository : IPaqueteTuristicoRepository
{
    private readonly TuristDbContext _context;

    public PaqueteTuristicoRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaqueteTuristico>> GetAllAsync()
    {
        return await _context.PaquetesTuristicos.ToListAsync();
    }

    public async Task<PaqueteTuristico?> GetByIdAsync(int id)
    {
        return await _context.PaquetesTuristicos.FindAsync(id);
    }

    public async Task AddAsync(PaqueteTuristico paquete)
    {
        await _context.PaquetesTuristicos.AddAsync(paquete);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PaqueteTuristico paquete)
    {
        _context.PaquetesTuristicos.Update(paquete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(PaqueteTuristico paquete)
    {
        _context.PaquetesTuristicos.Remove(paquete);
        await _context.SaveChangesAsync();
    }
    //============
    //Asignar Categorias
    //============
    public async Task LimpiarCategoriasAsync(int paqueteId)
    {
        var categoriasExistentes = await _context.Paquete_Categorias
            .Where(pc => pc.PaqueteID == paqueteId)
            .ToListAsync();

        if (categoriasExistentes.Any())
        {
            _context.Paquete_Categorias.RemoveRange(categoriasExistentes);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AsignarCategoriasAsync(List<Paquete_Categoria> categorias)
    {
        await _context.Paquete_Categorias.AddRangeAsync(categorias);
        await _context.SaveChangesAsync();
    }
}