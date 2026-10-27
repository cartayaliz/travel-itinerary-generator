using Microsoft.AspNetCore.Mvc;
using Turist.Data;
using Turist.Domain.Models;
using Turist.Services;

namespace Turist.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly TuristDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(TuristDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            // Buscamos al usuario por su ID en la tabla Usuarios
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioID == dto.UsuarioID);

            // Verificamos que el usuario exista y que la contraseña coincida usando BCrypt
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                return Unauthorized(new { message = "Credenciales inválidas (UsuarioID o contraseña incorrectos)" });
            }

            // Generamos el token JWT (el servicio deducirá automáticamente si es Guía, Jefe o Turista por herencia)
            var token = _tokenService.GenerateToken(usuario);

            return Ok(new 
            { 
                token, 
                usuarioID = usuario.UsuarioID,
                mensaje = "Autenticación exitosa" 
            });
        }
    }

    // DTO (Data Transfer Object) para recibir los datos del login
    public record LoginDto(int UsuarioID, string Password);
}