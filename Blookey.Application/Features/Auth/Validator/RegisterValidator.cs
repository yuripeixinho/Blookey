using Blookey.Application.Common.Validation;
using Blookey.Application.Features.Auth.Commands;
using FluentValidation;

namespace Blookey.Application.Features.Auth.Validator;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("email"))
            .MinimumLength(5).WithMessage(GenericMessages.TamanhoMinimo("email", 5));
    }
}
