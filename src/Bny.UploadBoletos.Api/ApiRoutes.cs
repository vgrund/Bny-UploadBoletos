using Bny.UploadBoletos.Application.Exceptions;
using Bny.UploadBoletos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
                        var operacoes = await _operacoesService.ProcessarArquivoAsync(arquivo);
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

                    return Results.Created(operacoes);
                }
            )
                .Produces<Operacao>(StatusCodes.Status201Created)
                .Produces<CustomProblemDetails>(StatusCodes.Status422UnprocessableEntity)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("UploadOperacoes")
                .WithTags("Upload","Operações Renda Variavel");
        }
    }
}
