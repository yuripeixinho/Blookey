using Blookey.Infrastructure.Integrations.Assas.Exceptions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blookey.Infrastructure.Integrations.Assas.Client;

public class AssasHttpClient
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };

    public AssasHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //public async Task<TResponse> GetAsync<TResponse>(string endpoint)
    //{
    //    var response = await _httpClient.GetAsync(endpoint);

    //    if (!response.IsSuccessStatusCode)
    //        await HandleErrorResponse(response);

    //    return await response.Content.ReadFromJsonAsync<TResponse>();
    //}

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, body, JsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
            await ThrowAssasException(response, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, cancellationToken);

        if (result is null)
            throw new AssasIntegrationException(200, "Asaas retornou resposta vazia.");

        return result;
    }

    private static async Task ThrowAssasException(HttpResponseMessage response, CancellationToken ct)
    {
        var body = await response.Content.ReadAsStringAsync(ct);

        throw response.StatusCode switch
        {
            HttpStatusCode.NotFound => new AssasNotFoundException(body),
            HttpStatusCode.BadRequest => new AssasBadRequestException(body),
            HttpStatusCode.Unauthorized => new AssasUnauthorizedException(body),
            HttpStatusCode.UnprocessableEntity => new AssasValidationException(body),
            _ => new AssasIntegrationException((int)response.StatusCode, body)
        };
    }
}
