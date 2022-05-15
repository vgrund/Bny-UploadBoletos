using Bny.UploadBoletos.Application.Exceptions;
using Bny.UploadBoletos.Application.Extensions;
using Bny.UploadBoletos.Application.Interfaces;
using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Bny.UploadBoletos.Application.Services
{
    public class OperacaoService : IOperacaoService
    {
        private readonly ILoteService _loteService;

        public OperacaoService(ILoteService loteService)
        {
            _loteService = loteService;
        }

        public async Task ProcessarArquivoAsync(IFormFile arquivo)
        {
            try
            {
                // var operacoes = new Dictionary<Guid, Operacao>(50);
                // var maiorOperacaoCliente = new Dictionary<string, Operacao>(50);
                var linhaCount = 0;
                
                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var linha = reader.ReadLine();
                        if (linha is null || 
                            string.Equals(linha, "0#RV") || 
                            string.Equals(linha, "99#RV"))
                        {
                            break;
                        }
                        
                        var dados = linha.Split("#", StringSplitOptions.None);

                        if (dados is null)
                        {
                            throw new FormatoArquivoInvalidoException($"Formato do arquivo inválido na linha {linhaCount}");
                        }

                        var operacao = new OperacaoBuilder()
                            .WithData(DateTime.ParseExact(dados.SafeGetValueByIndex(1), "yyyyMMdd", CultureInfo.InvariantCulture))
                            .WithCodigoCliente(dados.SafeGetValueByIndex(2))
                            .WithTipo(dados.SafeGetValueByIndex(3))
                            .WithIdBolsa(dados.SafeGetValueByIndex(4))
                            .WithCodigoAtivo(dados.SafeGetValueByIndex(5))
                            .WithCorretora(dados.SafeGetValueByIndex(6))
                            .WithQuantidade(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(7)) ? Convert.ToInt32(dados.SafeGetValueByIndex(7)) : 0)
                            .WithPrecoUnitario(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(8)) ? Convert.ToDecimal(dados.SafeGetValueByIndex(8)) : 0.00M)
                            .Build();

                        _loteService.AdicionaOperacao(operacao);
                        linhaCount++;
                    }
                }

                await GravarAsync();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private async Task GravarAsync()
        {


        }
    }
}
