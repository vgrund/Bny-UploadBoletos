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
        private readonly IOperacaoRepository _operacaoRepository;

        public OperacaoService(ILoteService loteService, IOperacaoRepository operacaoRepository)
        {
            _loteService = loteService;
            _operacaoRepository = operacaoRepository;
        }

        public async Task ProcessarArquivoAsync(IFormFile arquivo)
        {
            try
            {
                var linhaCount = 0;

                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var linha = reader.ReadLine();
                        if (linha is null || string.Equals(linha, String.Empty)) 
                            throw new FormatoArquivoInvalidoException($"Formato do arquivo inválido na linha {linhaCount}");

                        if (linha.EhInicioArquivo()) continue;
                        if (linha.EhFimArquivo()) break;
                        
                        var dados = linha.Split("#", StringSplitOptions.None);

                        if (dados is null)
                            throw new FormatoArquivoInvalidoException($"Formato do arquivo inválido na linha {linhaCount}");

                        var operacao = CriaOperacao(dados);

                        _loteService.AdicionaOperacao(operacao);
                        linhaCount++;
                    }
                }

                 await GravarAsync();
            }
            catch (FormatoArquivoInvalidoException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw new ErroInesperadoException("Algo inesperado ocorreu ao importar o arquivo");
            }
        }

        private Operacao CriaOperacao(string[] dados)
        {
            return new OperacaoBuilder()
                            .WithData(DateTime.ParseExact(dados.SafeGetValueByIndex(1), "yyyyMMdd", 
                                CultureInfo.InvariantCulture))
                            .WithCodigoCliente(dados.SafeGetValueByIndex(2))
                            .WithTipo(dados.SafeGetValueByIndex(3))
                            .WithIdBolsa(dados.SafeGetValueByIndex(4))
                            .WithCodigoAtivo(dados.SafeGetValueByIndex(5))
                            .WithCorretora(dados.SafeGetValueByIndex(6))
                            .WithQuantidade(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(7)) ?
                                Convert.ToInt32(dados.SafeGetValueByIndex(7)) : 0)
                            .WithPrecoUnitario(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(8)) ?
                                Convert.ToDecimal(dados.SafeGetValueByIndex(8)) : 0.00M)
                            .Build();
        }

        private async Task GravarAsync()
        {
            //foreach (var operacaoPair in _loteService.MaiorOperacaoCliente)
            //{
            //    await _operacaoRepository.AddAsync(operacaoPair.Value);
            //}

            //foreach (var operacaoPair in _loteService.DemaisOperacoes)
            //{
            //    await _operacaoRepository.AddAsync(operacaoPair.Value);
            //}

            await _operacaoRepository.AddRangeAsync(_loteService.MaiorOperacaoCliente.Values.ToList());
            await _operacaoRepository.AddRangeAsync(_loteService.DemaisOperacoes.Values.ToList());
        }
    }
}
