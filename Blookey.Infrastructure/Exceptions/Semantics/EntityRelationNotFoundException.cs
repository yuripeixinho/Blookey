namespace Blookey.Infrastructure.Exceptions.Semantics;

public class EntityRelationNotFoundException : Exception
{
    public EntityRelationNotFoundException(string entity, string ownerEntity, string ownerId) : base() { }

}
