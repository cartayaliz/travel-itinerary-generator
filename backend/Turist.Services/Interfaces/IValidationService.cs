namespace Turist.Services.Interfaces;

public interface IValidationService
{
    Task<bool> IsGuiaAuthorizedAsync(int guiaId, int paqueteId);
}