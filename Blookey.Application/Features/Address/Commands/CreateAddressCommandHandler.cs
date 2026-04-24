using Blookey.Domain.Identity;
using Blookey.Domain.Interfaces;
using MediatR;

namespace Blookey.Application.Features.Address.Commands;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, UserAddress>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;   

    public CreateAddressCommandHandler(IAddressRepository addressRepository, IUnitOfWork unitOfWork)
    {
        _addressRepository = addressRepository;
        _unitOfWork = unitOfWork;
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
            UserId = "ad1b578e-25d8-4aa9-a105-99c939c73bf3"
        };

        await _addressRepository.AddAsync(userAdress, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);  // UM commit, tudo ou nada

        return userAdress;
    }
}
