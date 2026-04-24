namespace Blookey.Infrastructure.Integrations.Assas.Client;

public class AssasClientOptions
{
    public const string Section = "Assas";    
    public string BaseUrl { get; set; } = string.Empty;   
    public string AccessToken { get; set; } = string.Empty;  
}

