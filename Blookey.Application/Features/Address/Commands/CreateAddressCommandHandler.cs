using Blookey.Application.Interfaces;
using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using MediatR;

namespace Blookey.Application.Features.Address.Commands;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, UserAddress>
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

    public async Task<UserAddress> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var userAdress = new UserAddress
        {
            Address = request.Address,
            AddressNumber = request.AddressNumber,
            Complement = request.Complement,
            Province = request.Province,
            PostalCode = request.PostalCode,
            UserId = _currentUser.Id
        };

        await _addressRepository.AddAsync(userAdress, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);  

        return userAdress;
    }
}
