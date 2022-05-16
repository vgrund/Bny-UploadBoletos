using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bny.UploadBoletos.Infra.Data.Repository
{
    public class OperacaoRepository : IOperacaoRepository
    {
        protected readonly BoletosContext Db;
        protected readonly DbSet<Operacao> DbSet;

        public OperacaoRepository(BoletosContext context)
        {
            Db = context;
            DbSet = Db.Set<Operacao>();
        }

        public async Task AddAsync(Operacao operacao, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(operacao);
            await Db.SaveChangesAsync();
        }

        public async Task<ICollection<Operacao>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
