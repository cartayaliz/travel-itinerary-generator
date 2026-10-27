using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class ParadaRepository : IParadaRepository
{
    private readonly TuristDbContext _context;

    public ParadaRepository(TuristDbContext context)
    {
        _context = context;
    }

    // 1. Obtener todas las paradas (Atracciones y Actividades)
    public async Task<IEnumerable<Parada>> GetAllAsync()
    {
        // EF Core traerá automáticamente Atracciones y Actividades gracias a la herencia
        return await _context.Paradas.ToListAsync();
    }

    // 2. Obtener una parada por su ID
    public async Task<Parada?> GetByIdAsync(int id)
    {
        return await _context.Paradas.FindAsync(id);
    }

    // 3. Guardar una nueva parada
    public async Task AddAsync(Parada parada)
    {
        await _context.Paradas.AddAsync(parada);
        await _context.SaveChangesAsync();
    }

    // 4. Actualizar una parada existente
    public async Task UpdateAsync(Parada parada)
    {
        _context.Paradas.Update(parada);
        await _context.SaveChangesAsync();
    }

    // 5. Eliminar una parada
    public async Task DeleteAsync(Parada parada)
    {
        _context.Paradas.Remove(parada);
        await _context.SaveChangesAsync();
    }

    // 6. Obtener paradas por categoría
    public async Task<IEnumerable<Parada>> GetByCategoryAsync(int categoriaId)
    {
        return await _context.Paradas
            .Where(p => p.CategoriaID == categoriaId)
            .ToListAsync();
    }
}