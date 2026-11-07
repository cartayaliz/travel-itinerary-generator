namespace Turist.Services.Interfaces;

// TResponse = Lo que devuelve (ej. ParadaResponseDto)
// TCreate = Lo que recibe para crear/actualizar (ej. ParadaCreateDto)
public interface ICrudService<TResponse, TCreate>
{
    Task<IEnumerable<TResponse>> GetAllAsync();
    Task<TResponse> GetByIdAsync(int id);
    Task CreateAsync(TCreate dto);
    Task UpdateAsync(int id, TCreate dto);
    Task DeleteAsync(int id);
}