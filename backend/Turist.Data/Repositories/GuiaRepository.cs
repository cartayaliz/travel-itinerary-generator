using Microsoft.EntityFrameworkCore;
using Turist.Domain.Interfaces.Repositories;
using Turist.Domain.Models;

namespace Turist.Data.Repositories;

public class GuiaRepository : IGuiaRepository
{
    private readonly TuristDbContext _context;

    public GuiaRepository(TuristDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.OfType<GuiaTuristico>().ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.OfType<GuiaTuristico>().FirstOrDefaultAsync(g => g.UsuarioID == id);
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task LimpiarPaquetesAutorizadosAsync(int guiaId)
    {
        var paquetes = await _context.Guia_Paquetes.Where(gp => gp.GuiaID == guiaId).ToListAsync();
        if (paquetes.Any())
        {
            _context.Guia_Paquetes.RemoveRange(paquetes);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AsignarPaquetesAutorizadosAsync(List<Guia_Paquete> paquetes)
    {
        await _context.Guia_Paquetes.AddRangeAsync(paquetes);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> IsAuthorizedForPackageAsync(int guiaId, int paqueteId)
    {
        return await _context.Guia_Paquetes
            .AnyAsync(gp => gp.GuiaID == guiaId && gp.PaqueteID == paqueteId);
    }
}