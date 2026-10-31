using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface ITuristaRepository : IUsuarioRepository
{
    Task LimpiarPaquetesAsync(int turistaId);
    Task AsignarPaquetesAsync(List<Turista_Paquete> paquetes);
}