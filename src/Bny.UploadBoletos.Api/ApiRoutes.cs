using Bny.UploadBoletos.Application.Exceptions;
using Bny.UploadBoletos.Application.Interfaces;
using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bny.UploadBoletos.Api
{
    public static class ApiRoutes
    {
        public static void ConfigureUploadRoutes(this WebApplication app)
        {
            app.MapPost("/v1/upload",
                [Consumes("multipart/form-data")]
                async ([FromForm] IFormFile arquivo,
                    IOperacaoService _operacoesService) =>
                {
                    if (arquivo.Length > 0)
                    {
                        return Results.BadRequest("Arquivo vazio ou inexistente");
                    }   

                    try
                    {
                        await _operacoesService.ProcessarArquivoAsync(arquivo);
                    }
                    catch (OperacaoInvalidaException e)
                    {
                        return Results.Problem(
                            new CustomProblemDetails(e.OperacaoInvalida.ValidationResult, 
                            StatusCodes.Status422UnprocessableEntity));
                    }
                    catch (Exception)
                    {
                        return Results.Problem("Um ou mais arquivos não foram importados.");
                    }

                    return Results.Created("", null);
                }
            )
                .Produces<Operacao>(StatusCodes.Status201Created)
                .Produces<CustomProblemDetails>(StatusCodes.Status422UnprocessableEntity)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PostUploadOperacoes")
                .WithTags("Operações Renda Variavel");

            app.MapGet("/v1/upload",
                async (IOperacaoRepository operacaoRepository) =>
                {
                    var operacoes = await operacaoRepository.GetAllAsync();
                    
                    if (!operacoes.Any()) return Results.NotFound();

                    return Results.Ok(await operacaoRepository.GetAllAsync());
                }
            )
                .Produces<List<Operacao>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetOperacoes")
                .WithTags("Operações Renda Variavel");
        }
    }
}
