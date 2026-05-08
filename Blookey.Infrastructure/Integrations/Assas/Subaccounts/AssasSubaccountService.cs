using Blookey.Application.Common.Interfaces;
using Blookey.Infrastructure.Integrations.Assas.Client;
using Blookey.Infrastructure.Integrations.Assas.Dtos;

namespace Blookey.Infrastructure.Integrations.Assas.Subaccounts;

public class AssasSubaccountService : IAssasSubaccountService
{
    private readonly AssasHttpClient _client;

    public AssasSubaccountService(AssasHttpClient cliente)
    {
        _client = cliente;
    }

    public async Task<CreateSubAccountResponse> CreateSubaccountAsync(CreateSubAccountRequest request, CancellationToken ct)
    {
        var response = await _client.PostAsync<CreateSubAccountRequest, CreateSubAccountResponse>("accounts", request);

        Console.WriteLine($"Subconta do cliente {request.Name} cadastrada com sucesso!");

        return response;
    }
}
