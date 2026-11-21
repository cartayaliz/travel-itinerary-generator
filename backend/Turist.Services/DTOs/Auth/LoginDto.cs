namespace Turist.Services.DTOs.Auth;

public record LoginDto(
    string UserName,
    string Password
);