using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;
using Turist.Data;
using Turist.Domain.Enums;
public class ActividadRepository : Repository<Actividad>, IActividadRepository
{

    public ActividadRepository(TuristDbContext context) : base(context)
    {
        
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