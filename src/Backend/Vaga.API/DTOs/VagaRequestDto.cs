using System;
using Vaga.Domain.Enums;

namespace Vaga.API.DTOs;

public record VagaRequestDto(
    string Titulo,
    string Descricao,
    TipoVaga TipoVaga,
    bool EhVagaAfirmativa,
    DateTime DataInicio,
    DateTime DataFim
);
