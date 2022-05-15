using Bny.UploadBoletos.Api;
using Bny.UploadBoletos.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
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
