using Blookey.Application.Features.Phone.Dtos;
using Blookey.Application.Interfaces;
using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Domain.Services;
using MediatR;

namespace Blookey.Application.Features.Phone.Commands;

public sealed class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, UserPhoneDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhoneRepository _phoneRepository;
    private readonly PhoneDomainService _domainService;

    public CreatePhoneCommandHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser, IPhoneRepository phoneRepository, PhoneDomainService domainService)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _phoneRepository = phoneRepository;
        _domainService = domainService;
    }

    public async Task<UserPhoneDto> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
    {
        await _domainService.EnsurePhoneIsUniqueAsync(request.Phone, _currentUser.Id, cancellationToken);

        var userPhone = UserPhone.Create(
            phone: request.Phone,
            phoneTypeId: request.PhoneType, 
            userId: _currentUser.Id
        );

        await _phoneRepository.AddAsync(userPhone, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UserPhoneDto.FromEntity(userPhone);
    }
}
