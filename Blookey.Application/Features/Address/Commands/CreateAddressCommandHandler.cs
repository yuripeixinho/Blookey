using Blookey.Application.Features.Address.Dtos;
using Blookey.Application.Interfaces;
using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using MediatR;

namespace Blookey.Application.Features.Address.Commands;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, UserAddressDto>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;   
    private readonly ICurrentUser _currentUser; 

    public CreateAddressCommandHandler(IAddressRepository addressRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _addressRepository = addressRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<UserAddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var userAddress = UserAddress.Create(
            request.Address,
            request.AddressNumber,
            request.Complement,
            request.Province,
            request.PostalCode,
            _currentUser.Id
        );

        await _addressRepository.AddAsync(userAddress, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);  

        return UserAddressDto.FromEntity(userAddress);
    }
}