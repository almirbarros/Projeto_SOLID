using Vaga.Domain.Enums;

namespace Vaga.Domain.Entities;

public class Vaga
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CriadaEm { get; init; } = DateTime.UtcNow;

    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public TipoVaga TipoVaga { get; private set; } 
    public bool EhVagaAfirmativa { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }

    // Construtor para criação de novas vagas com estado válido
    public Vaga(string titulo, string descricao, TipoVaga tipoVaga, bool ehVagaAfirmativa, DateTime dataInicio, DateTime dataFim)
    {
        Titulo = titulo;
        Descricao = descricao;
        TipoVaga = tipoVaga;
        EhVagaAfirmativa = ehVagaAfirmativa;
        DataInicio = dataInicio;
        DataFim = dataFim;
    }

    // Construtor necessário para o EF Core mapear os dados da Infra
    protected Vaga() { }

    // Métodos de negócio para alteração segura de estado
    public void AtualizarDados(string titulo, string descricao, TipoVaga tipoVaga, bool ehVagaAfirmativa)
    {
        Titulo = titulo;
        Descricao = descricao;
        TipoVaga = tipoVaga;
        EhVagaAfirmativa = ehVagaAfirmativa;
    }

    public void AlterarPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        DataInicio = dataInicio;
        DataFim = dataFim;
    }
}
