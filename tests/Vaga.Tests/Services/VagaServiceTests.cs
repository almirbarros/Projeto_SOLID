using System;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using Vaga.Domain.Validators;
using Vaga.Infra.Interfaces;
using Xunit;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.Tests.Services;

public class VagaServiceTests
{
    // Instancia o validador real para testar as regras de negócio do FluentValidation
    private readonly VagaValidator _validator = new();

    [Fact]
    public void Vaga_DeveFalhar_QuandoTipoVagaForInvalido()
    {
        // Arrange
        var vagaInvalida = new VagaClasse
        {
            Titulo = "Dev C#",
            Descricao = "Atuar com .NET Core",
            TipoVaga = "Metaverso", // Tipo não permitido pelo validador
            DataInicio = DateTime.Today,
            DataFim = DateTime.Today.AddDays(10)
        };

        // Act
        var resultado = _validator.Validate(vagaInvalida);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "TipoVaga");
    }

    [Fact]
    public void Vaga_DeveFalhar_QuandoDataFimForAnteriorADataInicio()
    {
        var vagaComDatasInvalidas = new VagaClasse
        {
            Titulo = "Engenheiro de Software",
            Descricao = "Vaga Tech Lead",
            TipoVaga = "Remoto",
            DataInicio = DateTime.Today,
            DataFim = DateTime.Today.AddDays(-5) // Erro: Término anterior ao início
        };

        // Act
        var resultado = _validator.Validate(vagaComDatasInvalidas);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "DataFim");
    }

    [Fact]
    public void Vaga_DevePassar_QuandoDadosEDatasForemValidos()
    {
        // Arrange - Garante o caminho feliz ("Happy Path") com o novo modelo de dados
        var vagaValida = new VagaClasse
        {
            Titulo = "Desenvolvedor .NET Sênior",
            Descricao = "Foco em arquitetura SOLID",
            TipoVaga = "Híbrido",
            DataInicio = DateTime.Today,
            DataFim = DateTime.Today.AddDays(30)
        };

        // Act
        var resultado = _validator.Validate(vagaValida);

        // Assert
        Assert.True(resultado.IsValid);
        Assert.Empty(resultado.Errors);
    }
}
