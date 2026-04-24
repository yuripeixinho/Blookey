using Blookey.Application.Common.Validation;
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

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("CPF"))
            .Must(ValidarCpf).WithMessage(GenericMessages.FormatoInvalido("CPF"));

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage(GenericMessages.CampoObrigatorio("data de nascimento"))
            .LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser no futuro.")
            .Must(BeOver18).WithMessage("É necessário ter pelo menos 18 anos.");

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


    private bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        // Remove caracteres não numéricos
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11) return false;

        // Elimina CPFs com todos os números iguais (ex: 111.111.111-11)
        if (new string(cpf[0], 11) == cpf) return false;

        // Cálculo dos dígitos verificadores
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2) resto = 0;
        else resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2) resto = 0;
        else resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    private bool BeOver18(DateTime birthDate)
    {
        return birthDate <= DateTime.Now.AddYears(-18);
    }
}