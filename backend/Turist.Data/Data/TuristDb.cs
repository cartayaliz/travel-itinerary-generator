using Microsoft.EntityFrameworkCore;
using Turist.Domain.Models;
using Turist.Domain.Repository;

namespace Turist.Data;

public class TouristRepository : ITouristRepository
{
    private readonly TuristDbContext _context;

    public TouristRepository(TuristDbContext context)
    {
        _context = context;
    }

    // 1. Obtener todos los turistas
    public async Task<IEnumerable<Turista>> GetAllAsync()
    {
        return await _context.Turistas.ToListAsync();
    }

    // 2. Obtener un turista por su ID (Guid)
    public async Task<Turista?> GetByIdAsync(Guid id)
    {
        return await _context.Turistas.FindAsync(id);
    }

    // 3. Agregar un nuevo turista
    public async Task AddAsync(Turista entity)
    {
        await _context.Turistas.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // 4. Actualizar un turista existente
    public async Task UpdateAsync(Turista entity)
    {
        _context.Turistas.Update(entity);
        await _context.SaveChangesAsync();
    }

    // 5. Eliminar un turista
    public async Task DeleteAsync(Turista entity)
    {
        _context.Turistas.Remove(entity);
        await _context.SaveChangesAsync();
    }

    // 6. Obtener turistas filtrados por su grupo de viaje
    public async Task<IEnumerable<Turista>> GetTouristsByTravelGroupAsync(int travelGroupId)
    {
        return await _context.Turistas
            .Where(t => t.GrupoID == travelGroupId)
            .ToListAsync(); // ToListAsync() es compatible al retornar Task<IEnumerable<Tourist>>
    }

    // 7. Regla de negocio: Impedir asignación doble de itinerarios en una fecha
    public async Task<bool> HasDoubleItineraryAssignmentAsync(int touristId, DateTime date)
    {
        // Verifica si el turista ya tiene una ejecución o asignación de itinerario registrada en esa misma fecha
        return await _context.Itinerario_Turista
            .AnyAsync(e => e.TuristaID == touristId && e.FechaAsignacion == date.Date);
    }
}