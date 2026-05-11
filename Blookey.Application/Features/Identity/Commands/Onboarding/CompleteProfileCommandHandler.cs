using Blookey.Application.Common.Interfaces;
using Blookey.Application.Interfaces;
using Blookey.Domain.Common;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Integrations.Assas.Dtos;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Onboarding;

public class CompleteProfileCommandHandler : IRequestHandler<CompleteProfileCommand, Result<CompleteProfileResponse>>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IAssasSubaccountService _assasSubaccountService;
    private readonly IAsaasKeyProtector _assasKeyProtector; // ← adiciona


    public CompleteProfileCommandHandler(
        ICurrentUser currentUser, 
        IUserRepository userRepository,
        IAddressRepository addressRepository, 
        IPhoneRepository phoneRepository, 
        IAssasSubaccountService assasSubaccountService,
        IAsaasKeyProtector assasKeyProtector
        )
    {
        _currentUser = currentUser;
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _phoneRepository = phoneRepository;
        _assasSubaccountService = assasSubaccountService;
        _assasKeyProtector = assasKeyProtector;
    }

    public async Task<Result<CompleteProfileResponse>> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByUserIdAsync(_currentUser.Id, cancellationToken);
        if (address is null)
            return Result.Failure<CompleteProfileResponse>(new Error("Address.Missing", "O endereço do usuário é obrigatório para criar a subconta."));

        var phone = await _phoneRepository.GetByUserIdAsync(_currentUser.Id, cancellationToken);
        if (phone is null)
            return Result.Failure<CompleteProfileResponse>(new Error("Phone.Missing", "O telefone do usuário é obrigatório."));

        var subAccountRequest = new CreateSubAccountRequest
        {
            Name = _currentUser.Name,
            Email = _currentUser.Email,
            LoginEmail = _currentUser.Email,
            CpfCnpj = _currentUser.CpfCnpj,
            IncomeValue = _currentUser.IncomeValue,
            BirthDate = _currentUser.BirthDate,
            MobilePhone = phone.Phone.Value,
            Address = address.Address,
            AddressNumber = address.AddressNumber,
            Complement = address.Complement,
            Province = address.Province,
            PostalCode = address.PostalCode.Value,

            Webhooks = new List<WebhookRequest>(),
        };

        var assasResponse = await _assasSubaccountService.CreateSubaccountAsync(subAccountRequest, cancellationToken);
        var cipher = _assasKeyProtector.Encrypt(assasResponse.ApiKey);

        await _userRepository.UpdateOnboardingInfoAsync(
            _currentUser.Id,
            assasResponse.Id,
            cipher,
            assasResponse.WalletId,
            assasResponse.AccountNumber.Agency,
            assasResponse.AccountNumber.Account,
            assasResponse.AccountNumber.AccountDigit,
            cancellationToken);

        var responseDto = new CompleteProfileResponse("Sub-conta Asass criado e configurado com sucesso!");

        return Result.Success(responseDto);
    }
}