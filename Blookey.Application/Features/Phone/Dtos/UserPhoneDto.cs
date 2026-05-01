using Blookey.Domain.Entities.Identity;

namespace Blookey.Application.Features.Phone.Dtos;

public record UserPhoneDto(int Id, string Phone, int PhoneTypeId)
{
    public static UserPhoneDto FromEntity(UserPhone entity) =>
        new(entity.Id, entity.Phone.Value, entity.PhoneTypeId);
}