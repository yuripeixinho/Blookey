using Blookey.Application.Common.Exceptions;
using Blookey.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;

namespace Blookey.Application.Services;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor; 
    }

    public string Id =>
           _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? throw new UnauthorizedException("Usuário não autenticado.");

    public string Email =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)
        ?? throw new UnauthorizedException("Usuário não autenticado.");

    public string Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue("name")
           ?? throw new UnauthorizedException("Usuário não autenticado.");

    public string BirthDate => DateTime.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("BirthDate")
    ?? throw new UnauthorizedException("Usuário não autenticado.")).ToString("yyyy-MM-dd");

    public string CpfCnpj => _httpContextAccessor.HttpContext?.User?.FindFirstValue("CpfCnpj")
           ?? throw new UnauthorizedException("Usuário não autenticado.");

    public decimal IncomeValue => decimal.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("IncomeValue"), CultureInfo.InvariantCulture, out var res)
        ? res
        : throw new UnauthorizedException("Usuário não autenticado.");

    
}
