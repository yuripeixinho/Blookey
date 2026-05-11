using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blookey.Infrastructure.Exceptions.Semantics;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entity, object id)
        : base($"{entity} com id '{id}' não foi encontrado.")
    { }
}
