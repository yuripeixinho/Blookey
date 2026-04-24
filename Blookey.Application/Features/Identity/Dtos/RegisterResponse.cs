namespace Blookey.Application.Features.Identity.Dtos;

public record RegisterResponse(
    string UserId, 
    string Name, 
    string Email
);
