using Turist.Domain.Enums;
using Turist.Domain.Models;
using Turist.Services.DTOs.Parada;

namespace Turist.Services.Mappers;

public static class ParadaMapper
{
    // Entidad -> DTO
    public static ParadaResponseDto ToDto(this Parada parada)
    {
        return parada switch
        {
            Atraccion atr => new ParadaResponseDto(
                atr.ParadaID, atr.Nombre, atr.Latitud, atr.Longitud, atr.CategoriaID, atr.GuiaID,
                TipoParada.Atraccion,
                atr.HorarioApertura, atr.HorarioCierre,
                null, null, null, null
            ),
            Actividad act => new ParadaResponseDto(
                act.ParadaID, act.Nombre, act.Latitud, act.Longitud, act.CategoriaID, act.GuiaID,
                TipoParada.Actividad,
                null, null,
                act.Tipo.ToString(), act.NivelEsfuerzo.ToString(), act.HoraInicio, act.DuracionMinutos
            ),
            _ => throw new ArgumentException("Tipo de parada no reconocido.")
        };
    }

    // DTO -> Entidad (Para Crear)
    public static Parada ToEntity(this ParadaCreateDto dto)
    {
        return dto.TipoParada switch
        {
            TipoParada.Atraccion => new Atraccion
            {
                Nombre = dto.Nombre,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                CategoriaID = dto.CategoriaID,
                GuiaID = dto.GuiaID,
                HorarioApertura = dto.HorarioApertura,
                HorarioCierre = dto.HorarioCierre
            },
            TipoParada.Actividad => new Actividad
            {
                Nombre = dto.Nombre,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                CategoriaID = dto.CategoriaID,
                GuiaID = dto.GuiaID,
                Tipo = Enum.Parse<TipoActividad>(dto.TipoActividad ?? "Visita"),
                NivelEsfuerzo = Enum.Parse<NivelEsfuerzo>(dto.NivelEsfuerzo ?? "Bajo"),
                HoraInicio = dto.HoraInicio ?? TimeOnly.MinValue,
                DuracionMinutos = dto.DuracionMinutos ?? 60
            },
            _ => throw new ArgumentException("Tipo de parada no soportado.")
        };
    }

    // Método para actualizar una entidad existente (Para el PUT)
    public static void UpdateEntity(this ParadaCreateDto dto, Parada paradaExistente)
    {
        paradaExistente.Nombre = dto.Nombre;
        paradaExistente.Latitud = dto.Latitud;
        paradaExistente.Longitud = dto.Longitud;
        paradaExistente.CategoriaID = dto.CategoriaID;
        paradaExistente.GuiaID = dto.GuiaID;

        if (paradaExistente is Atraccion atraccion)
        {
            atraccion.HorarioApertura = dto.HorarioApertura;
            atraccion.HorarioCierre = dto.HorarioCierre;
        }
        else if (paradaExistente is Actividad actividad)
        {
            actividad.Tipo = Enum.Parse<TipoActividad>(dto.TipoActividad ?? "Visita");
            actividad.NivelEsfuerzo = Enum.Parse<NivelEsfuerzo>(dto.NivelEsfuerzo ?? "Bajo");
            actividad.HoraInicio = dto.HoraInicio ?? TimeOnly.MinValue;
            actividad.DuracionMinutos = dto.DuracionMinutos ?? 60;
        }
    }
}