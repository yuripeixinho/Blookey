namespace Blookey.Application.Common.Validation;

public static class GenericMessages
{
    public static string CampoObrigatorio(string nomeCampo)
         => $"O campo '{nomeCampo}' é obrigatório.";

    public static string CampoNaoNulo(string nomeCampo)
            => $"O campo '{nomeCampo}' não pode ser nulo.";

    public static string TamanhoExato(string nomeCampo, int tamanho)
        => $"O campo '{nomeCampo}' deve conter exatamente {tamanho} caracteres.";

    public static string TamanhoEntre(string nomeCampo, int min, int max)
        => $"O campo '{nomeCampo}' deve ter entre {min} e {max} caracteres.";

    public static string TamanhoMinimo(string nomeCampo, int tamanho)
    => $"O campo '{nomeCampo}' deve ter pelo menos {tamanho} caracteres.";

    public static string TamanhoMaximo(string nomeCampo, int tamanho)
    => $"O campo '{nomeCampo}' deve ter até {tamanho} caracteres.";

    public static string FormatoInvalido(string nomeCampo)
        => $"O campo '{nomeCampo}' está em formato inválido.";
}
