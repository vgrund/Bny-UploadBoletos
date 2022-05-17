using Bny.UploadBoletos.Application.Exceptions;
using Bny.UploadBoletos.Application.Extensions;
using Bny.UploadBoletos.Application.Interfaces;
using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Buffers;
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

        public void ProcessarArquivo(IFormFile arquivo)
        {
            try
            {
                var linhaCount = 0;
                DateTime data;
                string linha;
                string codigoCliente;
                string tipo;
                string idBolsa;
                string codigoAtivo;
                string corretora;
                int quantidade;
                decimal precoUnitario;

                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        //var linha = reader.ReadLine();
                        if (linha is null || string.Equals(linha, String.Empty)) 
                            throw new FormatoArquivoInvalidoException($"Formato do arquivo inválido na linha {linhaCount}");

                        if (linha.EhInicioArquivo()) continue;
                        if (linha.EhFimArquivo()) break;

                        //var dados = linha.Split("#", StringSplitOptions.None);

                        /*
                         * Alternativa a implementação do Split. O ganho é que com o Span
                         * os dados são alocados na Stack e não na Heap, o que alivia a pressão no GC,
                         * tornando o código mais performatico
                         */
                        var linhaSpan = linha.AsSpan(linha.IndexOf('#') + 1);

                        // Data
                        var primeiraTralhaPos = linhaSpan.IndexOf('#');
                        data = DateTime.ParseExact(linhaSpan.Slice(0, primeiraTralhaPos), "yyyyMMdd",
                                CultureInfo.InvariantCulture);

                        // Codigo Cliente
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        codigoCliente = linhaSpan.Slice(0, primeiraTralhaPos).ToString();

                        // Tipo
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        tipo = linhaSpan.Slice(0, primeiraTralhaPos).ToString();

                        // Id Bolsa
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        idBolsa = linhaSpan.Slice(0, primeiraTralhaPos).ToString();

                        // Codigo Ativo
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        codigoAtivo = linhaSpan.Slice(0, primeiraTralhaPos).ToString();

                        // Corretora
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        corretora = linhaSpan.Slice(0, primeiraTralhaPos).ToString();

                        // Quantidade
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        int.TryParse(linhaSpan.Slice(0, primeiraTralhaPos), out quantidade);

                        // Preco Unitario
                        linhaSpan = linhaSpan.Slice(primeiraTralhaPos + 1);
                        primeiraTralhaPos = linhaSpan.IndexOf('#');
                        decimal.TryParse(linhaSpan.Slice(0, primeiraTralhaPos), out precoUnitario);

                        //if (dados is null)
                        //    throw new FormatoArquivoInvalidoException($"Formato do arquivo inválido na linha {linhaCount}");

                        var operacao = new OperacaoBuilder()
                            .WithData(data)
                            .WithCodigoCliente(codigoCliente)
                            .WithTipo(tipo)
                            .WithIdBolsa(idBolsa)
                            .WithCodigoAtivo(codigoAtivo)
                            .WithCorretora(corretora)
                            .WithQuantidade(quantidade)
                            .WithPrecoUnitario(precoUnitario)
                            .Build();

                        _loteService.AdicionaOperacao(operacao);
                        linhaCount++;
                    }
                }

                  Gravar();
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

        // Refatorado
        //private Operacao CriaOperacao(string[] dados)
        //{
        //    return new OperacaoBuilder()
        //                    .WithData(DateTime.ParseExact(dados.SafeGetValueByIndex(1), "yyyyMMdd", 
        //                        CultureInfo.InvariantCulture))
        //                    .WithCodigoCliente(dados.SafeGetValueByIndex(2))
        //                    .WithTipo(dados.SafeGetValueByIndex(3))
        //                    .WithIdBolsa(dados.SafeGetValueByIndex(4))
        //                    .WithCodigoAtivo(dados.SafeGetValueByIndex(5))
        //                    .WithCorretora(dados.SafeGetValueByIndex(6))
        //                    .WithQuantidade(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(7)) ?
        //                        Convert.ToInt32(dados.SafeGetValueByIndex(7)) : 0)
        //                    .WithPrecoUnitario(!string.IsNullOrEmpty(dados.SafeGetValueByIndex(8)) ?
        //                        Convert.ToDecimal(dados.SafeGetValueByIndex(8)) : 0.00M)
        //                    .Build();
        //}

        private void Gravar()
        {
            // Hipotese testada
            //foreach (var operacaoPair in _loteService.MaiorOperacaoCliente)
            //{
            //    await _operacaoRepository.AddAsync(operacaoPair.Value);
            //}

            //foreach (var operacaoPair in _loteService.DemaisOperacoes)
            //{
            //    await _operacaoRepository.AddAsync(operacaoPair.Value);
            //}

             _operacaoRepository.AddRange(_loteService.MaiorOperacaoCliente.Values.ToList());
             _operacaoRepository.AddRange(_loteService.DemaisOperacoes.Values.ToList());
        }

    }
}
