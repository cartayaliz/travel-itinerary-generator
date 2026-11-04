using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Data;
using Turist.Domain.Enums;
public class ActividadRepository : IActividadRepository
{
    protected readonly TuristDbContext _context;

    public ActividadRepository(TuristDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Actividad>> GetAllAsync() => await _context.Set<Actividad>().ToListAsync();

    public async Task<Actividad?> GetByIdAsync(int id) => await _context.Set<Actividad>().FindAsync(id);

    public async Task AddAsync(Actividad actividad)
    {
        await _context.Set<Actividad>().AddAsync(actividad);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Actividad actividad)
    {
        _context.Set<Actividad>().Update(actividad);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Actividad actividad)
    {
        _context.Set<Actividad>().Remove(actividad);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Actividad>> GetParaGeneracionAsync(
        NivelEsfuerzo? esfuerzoMaximo, 
        List<TipoActividad> tiposPermitidos)
    {
        var query = _context.Actividades
            .Include(a => a.Categoria) // Incluimos la categoría para la distribución
            .AsQueryable();

        // Aplicar filtro de esfuerzo máximo si se especificó
        if (esfuerzoMaximo.HasValue)
        {
            query = query.Where(a => a.NivelEsfuerzo <= esfuerzoMaximo.Value);
        }

        // Aplicar filtro de tipos permitidos si se especificaron
        if (tiposPermitidos != null && tiposPermitidos.Any())
        {
            query = query.Where(a => tiposPermitidos.Contains(a.Tipo));
        }

        return await query.ToListAsync();
    }
}