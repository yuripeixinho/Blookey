namespace Blookey.Infrastructure.Integrations.Assas.Exceptions;

public class AssasIntegrationException : Exception
{
    public int StatusCode { get; }
    public string ResponseBody { get; }

    public AssasIntegrationException(int statusCode, string body)
        : base($"Asaas retornou {statusCode}: {body}")
    {
        StatusCode = statusCode;
        ResponseBody = body;
    }
}

public class AssasNotFoundException : AssasIntegrationException
{
    public AssasNotFoundException(string body) : base(404, body) { }
}

public class AssasBadRequestException : AssasIntegrationException
{
    public AssasBadRequestException(string body) : base(400, body) { }
}

public class AssasUnauthorizedException : AssasIntegrationException
{
    public AssasUnauthorizedException(string body) : base(401, body) { }
}

public class AssasValidationException : AssasIntegrationException
{
    public AssasValidationException(string body) : base(422, body) { }
}