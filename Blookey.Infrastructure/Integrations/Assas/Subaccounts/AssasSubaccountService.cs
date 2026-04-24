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

    public async Task CreateSubaccountAsync(string userId, string name, string email)
    {
        var request = new CreateSubAccountRequest
        {
            Name = "Rodrigo Silva",
            Email = "orbrigadaoporajudar@exemplo.com",
            LoginEmail = "orbrigadaoporajudar@exemplo.com",

            CpfCnpj = "12345678909",
            BirthDate = "1990-08-25",
            Phone = "1133334444",
            MobilePhone = "11999998888",
            Site = "https://www.rodrigosilva.com.br",
            IncomeValue = 15000,
            Address = "Rua das Flores",
            AddressNumber = "123",
            Complement = "Apto 42",
            Province = "Centro",
            PostalCode = "01001-000",
            Webhooks = new List<WebhookRequest>()
        };

        var response = await _client.PostAsync<CreateSubAccountRequest, CreateSubAccountResponse>("accounts", request);

        Console.WriteLine($"Subconta do cliente {name} cadastrada com sucesso!");
    }

    //public async Task<IEnumerable<AssasCustomerDto>> ListCustomersAsync()
    //{
    //    var response = await _client
    //        .GetAsync<AssasListResponse<CustomerResponse>>("/customers");

    //    return response.Data.Select(c => new AssasCustomerDto(c.Id, c.Name, c.Email));
    //}
}
