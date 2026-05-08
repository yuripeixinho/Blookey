using Blookey.Infrastructure.Integrations.Assas.Dtos;

namespace Blookey.Application.Common.Interfaces;

public interface IAssasSubaccountService
{
    Task<CreateSubAccountResponse> CreateSubaccountAsync(
        CreateSubAccountRequest createSubAccountRequest,
        CancellationToken cancellationToken = default);
}
