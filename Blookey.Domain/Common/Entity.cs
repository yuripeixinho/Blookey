namespace Blookey.Domain.Common;

public class Entity
{
    public Guid Id { get; protected private set; } = Guid.NewGuid();
}
