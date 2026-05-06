namespace Blookey.Infrastructure.Integrations.Assas.Dtos;


public class CreateSubAccountRequest
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string? LoginEmail { get; set; }

    public string CpfCnpj { get; set; }

    /// <summary>
    /// Data de nascimento (somente quando Pessoa Física). Formato: YYYY-MM-DD
    /// </summary>
    public string? BirthDate { get; set; }

    /// <summary>
    /// Tipo da empresa (somente quando Pessoa Jurídica)
    /// </summary>
    public CompanyType? CompanyType { get; set; }

    public string? Phone { get; set; }

    public string MobilePhone { get; set; }

    public string? Site { get; set; }

    public decimal IncomeValue { get; set; }

    public string Address { get; set; }

    public string AddressNumber { get; set; }

    public string? Complement { get; set; }

    public string Province { get; set; }

    public string PostalCode { get; set; }

    public List<WebhookRequest>? Webhooks { get; set; } = new List<WebhookRequest>();
}

/// <summary>
/// Enumerações para o tipo de empresa
/// </summary>
public enum CompanyType
{
    MEI,
    LIMITED,
    INDIVIDUAL,
    ASSOCIATION
}

/// <summary>
/// Classe de suporte para a lista de Webhooks
/// </summary>
public class WebhookRequest
{
    // Propriedades do webhook
}



