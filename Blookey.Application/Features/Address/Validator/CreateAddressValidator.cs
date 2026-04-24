using Blookey.Application.Common.Validation;
using Blookey.Application.Features.Address.Commands;
using FluentValidation;

namespace Blookey.Application.Features.Address.Validator;

public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("Logradouro"))
            .MinimumLength(5).WithMessage(GenericMessages.TamanhoMinimo("Logradouro", 5))
            .MaximumLength(255).WithMessage(GenericMessages.TamanhoMaximo("Logradouro", 255));

        RuleFor(x => x.AddressNumber)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("Número"))
            .MaximumLength(20).WithMessage(GenericMessages.TamanhoMaximo("Número", 20));

        RuleFor(x => x.Complement)
            .MaximumLength(100).WithMessage(GenericMessages.TamanhoMaximo("Complemento", 100))
            .When(x => !string.IsNullOrEmpty(x.Complement));

        RuleFor(x => x.Province)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("Bairro/Província"))
            .MaximumLength(150).WithMessage(GenericMessages.TamanhoMaximo("Bairro/Província", 150));

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("CEP"))
            .Matches(@"^\d{5}-?\d{3}$").WithMessage(GenericMessages.FormatoInvalido("CEP"))
            .Length(8).WithMessage(GenericMessages.TamanhoExato("CEP", 8));
    }

}
