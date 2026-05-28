using Microsoft.EntityFrameworkCore;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VagaClasse> Vagas => Set<VagaClasse>();
}
