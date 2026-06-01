using System;
using System.Linq;
using FluentValidation;
using Moq;
using Vaga.Domain.Enums;
using Vaga.Domain.Validators;
using Vaga.Infra.Interfaces;
using Xunit;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.Tests.Services;

public class VagaServiceTests
{
    private readonly VagaValidator _validator = new();

    [Fact]
    public void Vaga_DeveFalhar_QuandoTipoVagaForInvalido()
    {
        // Arrange: Para testar um Enum inválido, fazemos o cast de um inteiro inexistente (ex: 99)
        var vagaInvalida = new VagaClasse(
            "Dev C#",
            "Atuar com .NET Core",
            (TipoVaga)99, 
            ehVagaAfirmativa: false,
            DateTime.Today,
            DateTime.Today.AddDays(10)
        );

        // Act
        var resultado = _validator.Validate(vagaInvalida);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "TipoVaga");
    }

    [Fact]
    public void Vaga_DeveFalhar_QuandoDataFimForAnteriorADataInicio()
    {
        // Arrange: Passa os dados pelo construtor respeitando o encapsulamento
        var vagaComDatasInvalidas = new VagaClasse(
            "Engenheiro de Software",
            "Vaga Tech Lead",
            TipoVaga.Remoto,
            ehVagaAfirmativa: false,
            DateTime.Today,
            DateTime.Today.AddDays(-5) // Término anterior ao início
        );

        // Act
        var resultado = _validator.Validate(vagaComDatasInvalidas);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "DataFim");
    }

    [Fact]
    public void Vaga_DevePassar_QuandoDadosEDatasForemValidos()
    {
        // Arrange: Caminho feliz ("Happy Path") usando o Enum fortemente tipado
        var vagaValida = new VagaClasse(
            "Desenvolvedor .NET Sênior",
            "Foco em arquitetura SOLID",
            TipoVaga.Hibrido,
            ehVagaAfirmativa: false,
            DateTime.Today,
            DateTime.Today.AddDays(30)
        );

        // Act
        var resultado = _validator.Validate(vagaValida);

        // Assert
        Assert.True(resultado.IsValid);
        Assert.Empty(resultado.Errors);
    }
}