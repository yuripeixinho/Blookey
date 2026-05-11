using Blookey.Application.Common.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace Blookey.Infrastructure.Security;

public sealed class AsaasKeyProtector : IAsaasKeyProtector
{
    private readonly IDataProtector _protector;

    public AsaasKeyProtector(IDataProtectionProvider provider)
    {
        // "purpose string" — isola essa chave das demais proteções da app
        _protector = provider.CreateProtector("Blookey.Assas.ApiKey");
    }

    public string Encrypt(string apiKey) => _protector.Protect(apiKey);
    public string Decrypt(string cipher) => _protector.Unprotect(cipher);
}


