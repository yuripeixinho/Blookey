using Blookey.Application.Common.Interfaces;
using Blookey.Domain.Identity.Events;
using MediatR;

namespace Blookey.Application.Features.Identity.Events;

public class CreateAssasCustomerSubAccountHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly IAssasSubaccountService _assasService;

    public CreateAssasCustomerSubAccountHandler(IAssasSubaccountService assasService)
    {
        _assasService = assasService;    
    }

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        await _assasService.CreateSubaccountAsync(notification.UserId, notification.Name, notification.Email);    
    }
}
