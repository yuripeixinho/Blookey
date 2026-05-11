namespace Blookey.Application.Common.Interfaces;

public interface IAsaasKeyProtector
{
    string Encrypt(string apiKey);
    string Decrypt(string cipher);
}
