using Blookey.Domain.Entities.Identity;
using Blookey.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blookey.Infrastructure.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                // Força JWT mesmo com AddIdentity configurado
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                      ValidateIssuer = true,
                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidateAudience = true,
                      ValidAudience = configuration["Jwt:Audience"],
                  };
              });

        // 👇 AddIdentityCore em vez de AddIdentity — não sobrescreve os schemes
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddSignInManager()                                 
        .AddRoles<IdentityRole>()                              // se usar roles
        .AddEntityFrameworkStores<BlookeyContext>()
        .AddDefaultTokenProviders()
        .AddErrorDescriber<PortugueseIdentityErrorDescriber>();

        return services;
    }
}

public class PortugueseIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new() { Code = nameof(DefaultError), Description = "Ocorreu um erro desconhecido." };
    public override IdentityError InvalidUserName(string? userName) => new() { Code = nameof(InvalidUserName), Description = $"O nome de usuário '{userName}' é inválido. Pode conter apenas letras ou dígitos." };
    public override IdentityError InvalidEmail(string? email) => new() { Code = nameof(InvalidEmail), Description = $"O e-mail '{email}' é inválido." };
    public override IdentityError DuplicateUserName(string userName) => new() { Code = nameof(DuplicateUserName), Description = $"O nome de usuário '{userName}' já está sendo utilizado." };
    public override IdentityError DuplicateEmail(string email) => new() { Code = nameof(DuplicateEmail), Description = $"O e-mail '{email}' já está sendo utilizado." };
    public override IdentityError PasswordTooShort(int length) => new() { Code = nameof(PasswordTooShort), Description = $"A senha deve ter no mínimo {length} caracteres." };
    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new() { Code = nameof(PasswordRequiresUniqueChars), Description = $"A senha deve conter pelo menos {uniqueChars} caracteres distintos." };
    public override IdentityError PasswordRequiresNonAlphanumeric() => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve conter pelo menos um caractere especial (ex: @, !, #)." };
    public override IdentityError PasswordRequiresDigit() => new() { Code = nameof(PasswordRequiresDigit), Description = "A senha deve conter pelo menos um dígito ('0'-'9')." };
    public override IdentityError PasswordRequiresLower() => new() { Code = nameof(PasswordRequiresLower), Description = "A senha deve conter pelo menos uma letra minúscula ('a'-'z')." };
    public override IdentityError PasswordRequiresUpper() => new() { Code = nameof(PasswordRequiresUpper), Description = "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z')." };

}