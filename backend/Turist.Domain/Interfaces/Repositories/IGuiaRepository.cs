using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IGuiaRepository : IUsuarioRepository
{
    Task<bool> IsAuthorizedForPackageAsync(int guiaId, int paqueteId);
    Task LimpiarPaquetesAutorizadosAsync(int guiaId);
    Task AsignarPaquetesAutorizadosAsync(List<Guia_Paquete> paquetes);
}