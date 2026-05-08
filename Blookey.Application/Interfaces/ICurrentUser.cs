namespace Blookey.Application.Interfaces;

public interface ICurrentUser 
{
    string Id { get; }
    string Name { get; }
    string Email { get; }
    string BirthDate { get; }
    string CpfCnpj { get; }
    decimal IncomeValue { get; }
}
