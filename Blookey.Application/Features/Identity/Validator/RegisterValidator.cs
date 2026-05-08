using Blookey.Application.Common.Validation;
using Blookey.Application.Common.Validation.Helpers;
using Blookey.Application.Features.Identity.Commands.Auth;
using FluentValidation;

namespace Blookey.Application.Features.Identity.Validator;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("nome"))
            .MinimumLength(5).WithMessage(GenericMessages.TamanhoMinimo("nome", 5));

        RuleFor(x => x.CpfCnpj)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("CPF/CNPJ"))
            .Must(CpfCnpjValidator).WithMessage(GenericMessages.FormatoInvalido("CPF/CNPJ"));

        RuleFor(x => x.BirthDate)
              .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("data de nascimento"))
              .LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser no futuro.")
              .Must(BeOver18).WithMessage("É necessário ter pelo menos 18 anos.");

        RuleFor(x => x.IncomeValue)
            .NotEmpty()
                .WithMessage(GenericMessages.CampoObrigatorio("renda mensal"))
            .GreaterThan(0)
                .WithMessage("A renda mensal deve ser maior que zero.")
            .PrecisionScale(18, 2, false)
                .WithMessage("O valor da renda possui um formato inválido.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("email"))
            .EmailAddress().WithMessage(GenericMessages.FormatoInvalido("email"));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("senha"))
            .MinimumLength(6).WithMessage(GenericMessages.TamanhoMinimo("senha", 6));

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("confirmação de senha"))
            .Equal(x => x.Password).WithMessage(GenericMessages.DeveSerIgual("confirmação de senha", "senha"));
    }

    public bool CpfCnpjValidator(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento)) return false;

        // Remove qualquer caractere que não seja número
        var numeros = new string(documento.Where(char.IsDigit).ToArray());

        return numeros.Length switch
        {
            11 => CpfValidator.Exec(numeros),
            14 => CnpjValidator.Exec(numeros),
            _ => false
        };
    }

    private bool BeOver18(DateTime birthDate)
    {
        return birthDate <= DateTime.Now.AddYears(-18);
    }
}