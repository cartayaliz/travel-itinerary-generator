using Turist.Services.DTOs.Usuario;

namespace Turist.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
    Task<UsuarioResponseDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}