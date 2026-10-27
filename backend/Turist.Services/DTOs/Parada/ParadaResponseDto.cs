using Turist.Domain.Enums; // Importante para usar el Enum

namespace Turist.Services.DTOs.Parada;

public record ParadaResponseDto(
    int ParadaID,
    string Nombre,
    double Latitud,
    double Longitud,
    int CategoriaID,
    int GuiaID,
    TipoParada TipoParada,
    
    // Opcionales (Atracción)
    TimeOnly? HorarioApertura,
    TimeOnly? HorarioCierre,
    
    // Opcionales (Actividad)
    string? TipoActividad, 
    string? NivelEsfuerzo, 
    TimeOnly? HoraInicio,
    int? DuracionMinutos
);