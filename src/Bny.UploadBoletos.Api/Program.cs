using Bny.UploadBoletos.Api;
using Bny.UploadBoletos.Infra.IoC;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddInfrastructure(builder.Configuration);
var serviceName = "Bny.UploadBoletos.Api";
var serviceVersion = "1.0.0";
//builder.Services.AddOpenTelemetryTracing(b =>
//{
//    b
//    //.AddSource(serviceName)
//    .SetResourceBuilder(
//        ResourceBuilder.CreateDefault()
//            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
//    .AddHttpClientInstrumentation()
//    .AddAspNetCoreInstrumentation()
//    .AddConsoleExporter()
//;
//});
//IServiceCollection serviceCollection = builder.Services.AddOpenTelemetryMetrics(b =>
//{
//    b
//    .AddPrometheusExporter(options =>
//    {
//        options.StartHttpListener = true;
//        // Use your endpoint and port here
//        options.HttpListenerPrefixes = new string[] { $"http://localhost:{9091}/" };
//        options.ScrapeResponseCacheDurationMilliseconds = 0;
//    })
//    .AddConsoleExporter()
//    .AddHttpClientInstrumentation()
//    .AddAspNetCoreInstrumentation()
//    .AddMeter("MyApplicationMetrics");
//    // The rest of your setup code goes here too
//});

builder.Services.AddOpenTelemetryTracing(
                (builder) => builder
                    .SetResourceBuilder(
                          ResourceBuilder.CreateDefault().AddService("example-app"))
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri("grafana-agent:55680");
                    }))
    ;
#endregion

var app = builder.Build();

/*
 * Organizando o código - Extraindo as rotas para um fonte separado, pois ao longo
 * do tempo este arquivo pode ficar grande demais e dificil de manter
 */
app.ConfigureUploadRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
