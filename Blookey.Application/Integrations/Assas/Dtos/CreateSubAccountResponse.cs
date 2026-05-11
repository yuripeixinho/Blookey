namespace Blookey.Application.Integrations.Assas.Dtos;

public class CreateSubAccountResponse
{
    /// <summary>
    /// Tipo de objeto (ex: "account")
    /// </summary>
    public string Object { get; set; }

    /// <summary>
    /// Identificador único da subconta no Asaas
    /// </summary>
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string? LoginEmail { get; set; }

    public string? Phone { get; set; }

    public string? MobilePhone { get; set; }

    public string Address { get; set; }

    public string AddressNumber { get; set; }

    public string? Complement { get; set; }

    public string Province { get; set; }

    public string PostalCode { get; set; }

    public string CpfCnpj { get; set; }

    public string? BirthDate { get; set; }

    /// <summary>
    /// Tipo de Pessoa: JURIDICA ou FISICA
    /// </summary>
    public string PersonType { get; set; }

    /// <summary>
    /// Tipo da empresa (somente quando Pessoa Jurídica)
    /// </summary>
    public string? CompanyType { get; set; }

    /// <summary>
    /// Identificador único da cidade no Asaas
    /// </summary>
    public int? City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string? TradingName { get; set; }

    public string? Site { get; set; }

    /// <summary>
    /// Identificador único da carteira para split ou transferências
    /// </summary>
    public string WalletId { get; set; }

    public AccountNumberInfo AccountNumber { get; set; }

    public CommercialInfoExpiration CommercialInfo { get; set; }

    public AccessTokenInfo AccessToken { get; set; }

    /// <summary>
    /// Chave de API gerada para a subconta
    /// </summary>
    public string ApiKey { get; set; }
}

public class AccountNumberInfo
{
    public string Agency { get; set; }
    public string Account { get; set; }
    public string AccountDigit { get; set; }
}

public class AccessTokenInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public string? ExpirationDate { get; set; }
    public string DateCreated { get; set; }
    public string? ProjectedExpirationDateByLackOfUse { get; set; }
}

public class CommercialInfoExpiration
{
    public bool IsExpired { get; set; }
    public string? ScheduledDate { get; set; }
}