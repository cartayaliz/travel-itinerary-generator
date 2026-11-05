namespace Turist.Services.Interfaces;

using Turist.Services.DTOs.Parada;

public interface IParadaService
{
    Task CrearParadaAsync(ParadaCreateDto dto);
}