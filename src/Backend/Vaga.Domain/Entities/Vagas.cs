using System;
using System.ComponentModel.DataAnnotations;

namespace Vaga.Domain.Entities;

public class Vaga
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O título da vaga é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição da vaga é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione o tipo de vaga.")]
    public string TipoVaga { get; set; } = string.Empty;

    public bool EhVagaAfirmativa { get; set; }

    [Required(ErrorMessage = "A data de início é obrigatória.")]
    public DateTime DataInicio { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "A data de término é obrigatória.")]
    public DateTime DataFim { get; set; } = DateTime.Today.AddDays(30); // Sugere 30 dias por padrão

    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
}
