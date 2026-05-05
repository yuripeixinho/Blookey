using Blookey.Domain.Common;
using Blookey.Domain.Interfaces;

namespace Blookey.Domain.Services;

public sealed class PhoneDomainService
{
    private readonly IPhoneRepository _phoneRepository;

    public PhoneDomainService(IPhoneRepository phoneRepository)
    {
        _phoneRepository = phoneRepository; 
    }

    public async Task<Result> EnsurePhoneIsUniqueAsync(string phone, string userId, CancellationToken cancellationToken)
    {
        var exists = await _phoneRepository.ExistsAsync(phone, userId, cancellationToken);

        if (exists)
            return Result.Failure(new Error(
                    "Phone.Duplicate",
                    "Este número de telefone já está cadastrado para o usuário.",
                    ErrorType.Conflict));

        return Result.Success();
    }
}
