namespace Blookey.Domain.Entities;

public class Entity
{
    public Guid Id { get; protected private set; } = Guid.NewGuid();
}
