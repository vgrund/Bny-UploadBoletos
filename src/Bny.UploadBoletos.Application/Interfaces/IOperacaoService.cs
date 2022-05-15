using Microsoft.AspNetCore.Http;

namespace Bny.UploadBoletos.Application.Interfaces
{
    public interface IOperacaoService
    {
        Task ProcessarArquivoAsync(IFormFile arquivo);
    }
}
