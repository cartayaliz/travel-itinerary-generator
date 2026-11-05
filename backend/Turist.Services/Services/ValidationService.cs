using Turist.Domain.Interfaces.Repositories;
using Turist.Services.Interfaces;

namespace Turist.Services.Services;

public class ValidationService : IValidationService
{
    private readonly IGuiaRepository _guiaRepository;

    public ValidationService(IGuiaRepository guiaRepository)
    {
        _guiaRepository = guiaRepository;
    }

    public async Task<bool> IsGuiaAuthorizedAsync(int guiaId, int paqueteId)
    {
        return await _guiaRepository.IsAuthorizedForPackageAsync(guiaId, paqueteId);
    }
}