using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bny.UploadBoletos.Infra.IoC
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API Upload Boletos",
                    Description = "Upload do arquivo de lote de operações em renda variável",
                    Version = "v1"
                });
            });
        }
    }
}