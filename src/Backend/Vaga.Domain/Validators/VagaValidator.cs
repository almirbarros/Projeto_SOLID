using System.Linq;
using FluentValidation;
using Vaga.Domain.Entities;

namespace Vaga.Domain.Validators;

public class VagaValidator : AbstractValidator<Entities.Vaga>
{
    // Lista de tipos permitidos na hora do cadastro
    private static readonly string[] TiposPermitidos = { "Remoto", "Híbrido", "Presencial" };

    public VagaValidator()
    {
        RuleFor(v => v.Titulo)
            .NotEmpty().WithMessage("O título da vaga é obrigatório.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(v => v.Descricao)
            .NotEmpty().WithMessage("A descrição da vaga é obrigatória.");

        RuleFor(v => v.TipoVaga)
            .NotEmpty().WithMessage("O tipo de vaga deve ser informado no cadastro.")
            .Must(tipo => TiposPermitidos.Contains(tipo))
            .WithMessage($"Tipo de vaga inválido. Escolha entre: {string.Join(", ", TiposPermitidos)}.");

        RuleFor(x => x.DataInicio)
            .NotEmpty().WithMessage("A data de início é obrigatória.");

        RuleFor(x => x.DataFim)
            .NotEmpty().WithMessage("A data de término é obrigatória.")
            .GreaterThanOrEqualTo(x => x.DataInicio)
            .WithMessage("A data de término não pode ser anterior à data de início.");
    }
}
