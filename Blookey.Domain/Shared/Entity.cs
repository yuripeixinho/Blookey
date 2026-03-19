namespace Blookey.Domain.Shared;

public class Entity
{
    public Guid Id { get; protected private set; } = Guid.NewGuid();
}
