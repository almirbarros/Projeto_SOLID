using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.Infra.Interfaces;

public interface IVagaRepository
{
    Task<IEnumerable<VagaClasse>> ListarTodasAsync();
    Task<VagaClasse?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(VagaClasse vaga);
    Task AtualizarAsync(VagaClasse vaga);
    Task RemoverAsync(VagaClasse vaga);
}
