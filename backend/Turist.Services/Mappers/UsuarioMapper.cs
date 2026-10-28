using Turist.Domain.Enums;
using Turist.Domain.Models;
using Turist.Services.DTOs.Usuario;

namespace Turist.Services.Mappers;

public static class UsuarioMapper
{
    public static UsuarioResponseDto ToDto(this Usuario usuario)
    {
        return usuario switch
        {
            GuiaTuristico guia => new UsuarioResponseDto(
                guia.UsuarioID, guia.Nombre, TipoUsuario.Guia, guia.Especialidad, null, null
            ),
            Turista turista => new UsuarioResponseDto(
                turista.UsuarioID, turista.Nombre, TipoUsuario.Turista, null, turista.Edad, turista.GrupoID
            ),
            JefeOperaciones jefe => new UsuarioResponseDto(
                jefe.UsuarioID, jefe.Nombre, TipoUsuario.JefeOperaciones, null, null, null
            ),
            _ => throw new ArgumentException("Tipo de usuario no reconocido.")
        };
    }

    public static Usuario ToEntity(this UsuarioCreateDto dto)
    {
        // Hash de la contraseña
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        return dto.TipoUsuario switch
        {
            TipoUsuario.Guia => new GuiaTuristico
            {
                Nombre = dto.Nombre,
                PasswordHash = passwordHash,
                Especialidad = dto.Especialidad ?? "General"
            },
            TipoUsuario.Turista => new Turista
            {
                Nombre = dto.Nombre,
                PasswordHash = passwordHash,
                Edad = dto.Edad ?? 0,
                GrupoID = dto.GrupoID ?? 1 // Asumimos que existe el grupo 1
            },
            TipoUsuario.JefeOperaciones => new JefeOperaciones
            {
                Nombre = dto.Nombre,
                PasswordHash = passwordHash
            },
            _ => throw new ArgumentException("Tipo de usuario no soportado.")
        };
    }
    public static void UpdateEntity(this UsuarioCreateDto dto, Usuario usuarioExistente)
    {
        // Actualizamos los campos base
        usuarioExistente.Nombre = dto.Nombre;

        // Si la contraseña no viene vacía, la actualizamos (Buena práctica)
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            usuarioExistente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }

        // Actualizamos los campos específicos según el tipo
        if (usuarioExistente is GuiaTuristico guia)
        {
            guia.Especialidad = dto.Especialidad ?? guia.Especialidad;
        }
        else if (usuarioExistente is Turista turista)
        {
            turista.Edad = dto.Edad ?? turista.Edad;
            turista.GrupoID = dto.GrupoID ?? turista.GrupoID;
        }
    }
}