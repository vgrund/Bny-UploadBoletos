using Bny.UploadBoletos.Domain.OperacoesAggregate;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Bny.UploadBoletos.Infra.Data
{
    public class BoletosContext: DbContext
    {
        public BoletosContext(DbContextOptions<BoletosContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Operacao> Operacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoletosContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
