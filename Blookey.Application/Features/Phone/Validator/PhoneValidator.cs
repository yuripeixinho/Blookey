using Blookey.Application.Common.Validation;
using Blookey.Application.Features.Phone.Commands;
using FluentValidation;

namespace Blookey.Application.Features.Phone.Validator;

public class PhoneValidator : AbstractValidator<CreatePhoneCommand>
{
    public PhoneValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("telefone"))
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("O número de telefone deve estar em formato internacional válido.");

        RuleFor(x => x.PhoneType)
            .NotEmpty().WithMessage("O tipo de telefone é obrigatório.");

    }  
}
