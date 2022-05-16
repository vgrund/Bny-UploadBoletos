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
                //[Consumes("multipart/form-data")]
                async ([FromForm] HttpRequest request,
                    IOperacaoService _operacoesService
                    ) =>
                {
                    if (!request.HasFormContentType)
                        return Results.BadRequest("Content-Type inválido");

                    var formCollection = await request.ReadFormAsync();
                    var formFile = formCollection.Files["arquivos"];

                    if (formFile is null || formFile.Length == 0)
                        return Results.BadRequest("Arquivo vazio ou inexistente");

                    try
                    {
                        await _operacoesService.ProcessarArquivoAsync(formFile);
                        return Results.Created("", null);
                    }
                    catch (FormatoArquivoInvalidoException e)
                    {
                        return Results.Problem(e.Message, statusCode: StatusCodes.Status422UnprocessableEntity);
                    }
                    catch (ErroInesperadoException e)
                    {
                        return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
                    }
                }
            )
                .Produces(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PostUploadOperacoes")
                .WithTags("Operações Renda Variavel")
                .Accepts<IFormFile>(contentType: "multipart/form-data")
                ;

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

            app.MapDelete("/v1/upload",
                (IOperacaoRepository operacaoRepository) =>
                {
                    operacaoRepository.DeleteAll();

                    return Results.NoContent();
                }
            )
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteOperacoes")
                .WithTags("Operações Renda Variavel");
        }
    }
}
