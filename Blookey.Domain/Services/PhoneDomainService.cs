using Blookey.Domain.Exceptions;
using Blookey.Domain.Interfaces;

namespace Blookey.Domain.Services;

public sealed class PhoneDomainService
{
    private readonly IPhoneRepository _phoneRepository;

    public PhoneDomainService(IPhoneRepository phoneRepository)
    {
        _phoneRepository = phoneRepository; 
    }

    public async Task EnsurePhoneIsUniqueAsync(string phone, string userId, CancellationToken cancellationToken)
    {
        var exists = await _phoneRepository.ExistsAsync(phone, userId, cancellationToken);

        if (exists)
            throw new DomainException("Este número de telefone já está cadastrado para o usuário.");
    }
}
