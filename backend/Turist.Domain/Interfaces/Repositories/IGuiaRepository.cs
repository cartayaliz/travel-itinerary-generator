using Turist.Domain.Models;

namespace Turist.Domain.Interfaces.Repositories;

public interface IGuiaRepository : IUsuarioRepository
{
    Task LimpiarPaquetesAutorizadosAsync(int guiaId);
    Task AsignarPaquetesAutorizadosAsync(List<Guia_Paquete> paquetes);
}