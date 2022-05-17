using Microsoft.AspNetCore.Http;

namespace Bny.UploadBoletos.Application.Interfaces
{
    public interface IOperacaoService
    {
        void ProcessarArquivo(IFormFile arquivo);
    }
}
