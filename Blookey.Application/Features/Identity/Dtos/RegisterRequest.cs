namespace Blookey.Application.Features.Identity.Dtos;

public record RegisterRequest(
    string Name, 
    string CPF, 
    DateTime BirthDate, 
    string Email, 
    string Password, 
    string ConfirmPassword
);