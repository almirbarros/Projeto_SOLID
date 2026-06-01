using FluentValidation;

namespace Vaga.Domain.Validators;

public class VagaValidator : AbstractValidator<Entities.Vaga>
{
    public VagaValidator()
    {
        RuleFor(v => v.Titulo)
            .NotEmpty().WithMessage("O título da vaga é obrigatório.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(v => v.Descricao)
            .NotEmpty().WithMessage("A descrição da vaga é obrigatória.");

        RuleFor(v => v.TipoVaga)
            .IsInEnum().WithMessage("Tipo de vaga inválido. Selecione uma das opções disponíveis.");

        RuleFor(x => x.DataInicio)
            .NotEmpty().WithMessage("A data de início é obrigatória.");

        RuleFor(x => x.DataFim)
            .NotEmpty().WithMessage("A data de término é obrigatória.")
            .GreaterThanOrEqualTo(x => x.DataInicio)
            .WithMessage("A data de término não pode ser anterior à data de início.");
    }
}
