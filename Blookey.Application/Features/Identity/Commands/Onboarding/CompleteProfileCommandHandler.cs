using Blookey.Application.Common.Interfaces;
using Blookey.Application.Interfaces;
using Blookey.Domain.Common;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Integrations.Assas.Dtos;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Onboarding;

public class CompleteProfileCommandHandler : IRequestHandler<CompleteProfileCommand, Result<string>>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAddressRepository _addressRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IAssasSubaccountService _assasSubaccountService;

    public CompleteProfileCommandHandler(
        ICurrentUser currentUser, 
        IAddressRepository addressRepository, 
        IPhoneRepository phoneRepository, 
        IAssasSubaccountService assasSubaccountService)
    {
        _currentUser = currentUser;
        _addressRepository = addressRepository;
        _phoneRepository = phoneRepository;
        _assasSubaccountService = assasSubaccountService;
    }

    public async Task<Result<string>> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByUserIdAsync(_currentUser.Id, cancellationToken);
        var phone = await _phoneRepository.GetByUserIdAsync(_currentUser.Id, cancellationToken);

        var subAccountRequest = new CreateSubAccountRequest
        {
            Name = "teste1",
            Email = "teste1@exemplo.com",
            LoginEmail = "teste1@exemplo.com",

            CpfCnpj = "12345678909",
            BirthDate = "1990-08-25",
            Phone = "1133334444",
            Site = "https://www.rodrigosilva.com.br",
            IncomeValue = 15000,

            Webhooks = new List<WebhookRequest>(),

           //Name = _currentUser.Name,
           //Email = _currentUser.Email,
           //CpfCnpj = _currentUser.CpfCnpj,
           //BirthDate = _currentUser.BirthDate,
           MobilePhone = phone.Phone.Value,
            //IncomeValue = request.IncomeValue,
            //CompanyType = request.CompanyType,
            //Site = request.Site,
            Address = address.Address,
            AddressNumber = address.AddressNumber,
            Complement = address.Complement,
            Province = address.Province,
            PostalCode = address.PostalCode.Value
        };

        var assasResponse = await _assasSubaccountService.CreateSubaccountAsync(subAccountRequest);



        var teste = assasResponse;

        throw new NotImplementedException();
    }
}