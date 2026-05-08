namespace Blookey.Application.Features.Identity.Dtos;

public record RegisterRequest(
    string Name, 
    string CpfCnpj,
    DateTime BirthDate,
    decimal IncomeValue, 
    string Email, 
    string Password, 
    string ConfirmPassword
);