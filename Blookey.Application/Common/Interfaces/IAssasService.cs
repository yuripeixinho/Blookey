namespace Blookey.Application.Common.Interfaces;

public interface IAssasSubaccountService
{
    Task CreateSubaccountAsync(string userId, string name, string email);
}
