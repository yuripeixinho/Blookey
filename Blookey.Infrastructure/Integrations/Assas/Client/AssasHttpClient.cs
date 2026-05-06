using Blookey.Infrastructure.Integrations.Assas.Exceptions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace Blookey.Infrastructure.Integrations.Assas.Client;

public class AssasHttpClient
{
    private readonly HttpClient _http;    

    public AssasHttpClient(HttpClient http, IOptions<AssasClientOptions> options)
    {
        _http = http;
        _http.BaseAddress = new Uri(options.Value.BaseUrl);
        _http.DefaultRequestHeaders.Add("access_token", options.Value.AccessToken);
        _http.DefaultRequestHeaders.Add("User-Agent", "Blookey/1.0");
    }

    private async Task HandleErrorResponse(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"[ASSAS ERROR] Status: {response.StatusCode} | Body: {body}");

        throw response.StatusCode switch 
        {
            HttpStatusCode.NotFound => new AssasNotFoundException(body),
            HttpStatusCode.BadRequest => new AssasBadRequestException(body),
            HttpStatusCode.Unauthorized => new AssasUnauthorizedException(body),
            HttpStatusCode.UnprocessableEntity => new AssasValidationException(body),
            _ => new AssasIntegrationException((int)response.StatusCode, body)
        };
    }

    public async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        var response = await _http.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
            await HandleErrorResponse(response);

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest body)
    {
        var response = await _http.PostAsJsonAsync(endpoint, body);

        if (!response.IsSuccessStatusCode)
            await HandleErrorResponse(response);

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}
