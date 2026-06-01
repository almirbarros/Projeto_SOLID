namespace Vaga.Blazor.Models;

public record VagaResponseDto(
    Guid Id,
    string Titulo,
    string Descricao,
    Domain.Enums.TipoVaga TipoVaga,
    bool EhVagaAfirmativa,
    DateTime DataInicio,
    DateTime DataFim,
    DateTime CriadaEm
);
