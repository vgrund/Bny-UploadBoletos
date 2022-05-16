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
            await DbSet.AddAsync(operacao, cancellationToken);
            await Db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(ICollection<Operacao> operacoes, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(operacoes, cancellationToken);
            await Db.SaveChangesAsync();
        }

        public async Task<ICollection<Operacao>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public void DeleteAll()
        {
            DbSet.RemoveRange(DbSet);
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
