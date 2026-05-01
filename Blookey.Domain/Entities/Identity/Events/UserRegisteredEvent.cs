using MediatR;

namespace Blookey.Domain.Entities.Identity.Events;

public class UserRegisteredEvent : INotification
{
    public string UserId { get; }   
    public string Name { get; }
    public string Email { get; }

    public UserRegisteredEvent(string userId, string name, string email)
    {
        UserId = userId;
        Name = name;
        Email = email;
    }
}
