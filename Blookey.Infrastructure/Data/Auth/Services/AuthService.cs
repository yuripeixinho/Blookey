using Blookey.Application.Common.Exceptions;
using Blookey.Application.Common.Interfaces;
using Blookey.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blookey.Infrastructure.Data.Auth.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null) return null;

        // Verifica a senha e se não está bloqueado
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        // 1. Definir as Claims (Dados que vão dentro do Token)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), // ID do Usuário (Padrão)
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID do Token
            new Claim("nome_completo", user.UserName) // Claim Personalizada
        };

        // 2. Obter a chave secreta do appsettings.json
        // A chave deve ter pelo menos 32 caracteres (256 bits) para o algoritmo HmacSha256
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // 3. Gerar as credenciais de assinatura
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 4. Configurar a expiração (Ex: 2 horas)
        var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

        // 5. Montar o token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        // 6. Serializar para string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> RegisterAsync(string username, string email, string password)
    {
        // 1. PREPARAÇÃO DA ENTIDADE
        // Estamos mapeando os dados de entrada para a entidade do Identity.
        var user = new User
        {
            UserName = email,       // No Identity, UserName é o identificador de login. Usamos o email.
            Email = email,          // O email para contato/recuperação.
           
            SecurityStamp = Guid.NewGuid().ToString()  // SecurityStamp: Garante que se o usuário mudar dados sensíveis, tokens antigos morrem.
        };

        // 2. A MÁGICA DO IDENTITY (CreateAsync)
        // Este único método faz 3 coisas sequenciais:
        // A. Validações (Senha forte? Email duplicado? Caracteres inválidos?)
        // B. Hashing (Transforma "123456" em "AQAAAAEAACcQAAAA...")
        // C. Persistência (INSERT na tabela AspNetUsers)
        var result = await _userManager.CreateAsync(user, password);

        // 3. VERIFICAÇÃO DE SUCESSO
        // O Identity não lança Exception para falhas de validação (ex: senha curta).
        // Ele retorna result.Succeeded = false e preenche result.Errors.
        if (!result.Succeeded)
            throw new IdentityValidationException(result.Errors);

        // 4. RETORNO POSITIVO
        // Se chegou aqui, o usuário está gravado no banco. Retornamos o ID gerado (GUID).
        return $"Usuário {user.UserName} criado com sucesso!";
    }
}
