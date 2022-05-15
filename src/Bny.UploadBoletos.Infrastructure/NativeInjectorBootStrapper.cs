using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bny.UploadBoletos.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile($"appsettings.json", optional: true)
                    .Build();

            services.AddScoped<BoletosContext>();

            services.AddScoped<IOperacaoService, OperacaoService>();
            services.AddScoped<IOperacaoRepository, OperacaoRepository>();

            services.AddDbContext<BoletosContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
