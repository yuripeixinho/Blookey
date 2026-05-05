using Blookey.Application.Features.Phone.Dtos;
using Blookey.Application.Interfaces;
using Blookey.Domain.Common;
using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Domain.Services;
using MediatR;

namespace Blookey.Application.Features.Phone.Commands;

public sealed class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, Result<UserPhoneDto>>
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

    public async Task<Result<UserPhoneDto>> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
    {
        var uniqueResult = await _domainService.EnsurePhoneIsUniqueAsync(request.Phone, _currentUser.Id, cancellationToken);

        if (uniqueResult.IsFailure)
            return Result.Failure<UserPhoneDto>(uniqueResult.Error);

        var userPhone = UserPhone.Create(
            request.Phone,
            request.PhoneType, 
            _currentUser.Id
        );

        await _phoneRepository.AddAsync(userPhone, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(UserPhoneDto.FromEntity(userPhone));
    }
}
