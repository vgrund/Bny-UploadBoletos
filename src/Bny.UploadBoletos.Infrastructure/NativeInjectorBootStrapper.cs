using Bny.UploadBoletos.Application.Interfaces;
using Bny.UploadBoletos.Application.Services;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Services;
using Bny.UploadBoletos.Infra.Data;
using Bny.UploadBoletos.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bny.UploadBoletos.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile($"appsettings.json", optional: true)
                    .Build();

            services.AddScoped<BoletosContext>();

            var tamanhoMedioLote = config.GetValue<int>("TamanhoMedioLote");

            services.AddScoped<IOperacaoService, OperacaoService>();
            services.AddScoped<ILoteService>(s => new LoteService(tamanhoMedioLote));
            services.AddScoped<IOperacaoRepository, OperacaoRepository>();

            services.AddDbContext<BoletosContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
