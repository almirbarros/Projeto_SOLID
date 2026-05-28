using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vaga.Infra.Context;
using Vaga.Infra.Interfaces;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.Infra.Repositories;

public class VagaRepository : IVagaRepository
{
    private readonly AppDbContext _context;

    public VagaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<VagaClasse>> ListarTodasAsync()
        => await _context.Vagas.AsNoTracking().ToListAsync();

    public async Task<VagaClasse?> ObterPorIdAsync(Guid id)
        => await _context.Vagas.FindAsync(id);

    public async Task AdicionarAsync(VagaClasse vaga)
    {
        await _context.Vagas.AddAsync(vaga);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(VagaClasse vagaAtualizada)
    {
        // Busca a entidade original que já está sendo rastreada pelo Contexto
        var vagaExiste = await _context.Vagas.FindAsync(vagaAtualizada.Id);

        if (vagaExiste != null)
        {
            // Atualiza os valores da entidade monitorada com os novos dados recebidos
            _context.Entry(vagaExiste).CurrentValues.SetValues(vagaAtualizada);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoverAsync(VagaClasse vaga)
    {
        _context.Vagas.Remove(vaga);
        await _context.SaveChangesAsync();
    }
}
