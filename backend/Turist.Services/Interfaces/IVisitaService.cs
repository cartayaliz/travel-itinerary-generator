using Turist.Services.DTOs.Visita;

namespace Turist.Services.Interfaces;

public interface IVisitaService : ICrudService<VisitaResponseDto, VisitaCreateDto>
{
   
//nuevos
    Task<IEnumerable<VisitaResponseDto>> GetByTuristaIdAsync(int turistaId);

    Task RegistrarVisitaAsync(RegistrarVisitaDto dto, int turistaId); //que el controlador pase el ID extraído del token
}