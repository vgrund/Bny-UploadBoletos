using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Enums;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Services;
using FluentAssertions;
using System;
using Xunit;

namespace Bny.UploadBoletos.UnitTests.DomainTests
{
    public class LoteServiceTests
    {
        LoteService loteService;

        public LoteServiceTests()
        {
            ResetDados();
        }
        
        void ResetDados()
        {
            this.loteService = new LoteService(10);
        }

        [Fact]
        public void DeveCriticarTamanhoZero()
        {
            Action act = () =>
            {
                _ = new LoteService(0);
            };

            act.Should().Throw<ArgumentException>()
                .WithMessage("Tamanho não deve ser zero (Parameter 'tamanho')");
        }

        [Fact]
        public void DeveProcessarBoletoComplexoCorretamente()
        {
            var data = DateTime.Now;
            var tipo = "Compra";
            var codigoCliente = "CARTEIRA CLIENTE C";
            var codigoAtivo = "VALE3";
            var idBolsa = "BVSP";
            var corretora = "AGORA";
            var quantidade = 100;
            var precoUnitario = 43M;

            var operacao1 = new OperacaoBuilder()
                .WithData(data)
                .WithCodigoCliente(codigoCliente)
                .WithTipo(tipo)
                .WithIdBolsa(idBolsa)
                .WithCodigoAtivo(codigoAtivo)
                .WithCorretora(corretora)
                .WithQuantidade(quantidade)
                .WithPrecoUnitario(precoUnitario)
                .Build();

            codigoAtivo = "PETR4";
            quantidade = 1000;
            precoUnitario = 30m;
            var operacao2 = new OperacaoBuilder()
                .WithData(data)
                .WithCodigoCliente(codigoCliente)
                .WithTipo(tipo)
                .WithIdBolsa(idBolsa)
                .WithCodigoAtivo(codigoAtivo)
                .WithCorretora(corretora)
                .WithQuantidade(quantidade)
                .WithPrecoUnitario(precoUnitario)
                .Build();

            codigoCliente = "CARTEIRA CLIENTE B";
            quantidade = 1000;
            precoUnitario = 30m;
            codigoAtivo = "PETRO";
            var operacao3 = new OperacaoBuilder()
                .WithData(data)
                .WithCodigoCliente(codigoCliente)
                .WithTipo(tipo)
                .WithIdBolsa(idBolsa)
                .WithCodigoAtivo(codigoAtivo)
                .WithCorretora(corretora)
                .WithQuantidade(quantidade)
                .WithPrecoUnitario(precoUnitario)
                .Build();

            codigoCliente = "CARTEIRA CLIENTE B";
            codigoAtivo = "PETRO";
            quantidade = 500;
            precoUnitario = 100m;
            var operacao4 = new OperacaoBuilder()
                .WithData(data)
                .WithCodigoCliente(codigoCliente)
                .WithTipo(tipo)
                .WithIdBolsa(idBolsa)
                .WithCodigoAtivo(codigoAtivo)
                .WithCorretora(corretora)
                .WithQuantidade(quantidade)
                .WithPrecoUnitario(precoUnitario)
                .Build();

            loteService.AdicionaOperacao(operacao1);
            loteService.AdicionaOperacao(operacao2);
            loteService.AdicionaOperacao(operacao3);
            loteService.AdicionaOperacao(operacao4);
            
            // Testa as Maiores Operações de cada cliente
            loteService.MaiorOperacaoCliente[operacao2.CodigoCliente].ValorDesconto
                .Should()
                .Be(operacao2.ValorFinanceiro * 0.9M);

            loteService.MaiorOperacaoCliente[operacao2.CodigoCliente].StatusBoleto
                .Should()
                .Be(OperacaoStatusBoleto.OK.DisplayName);

            loteService.MaiorOperacaoCliente[operacao2.CodigoCliente].Mensagem
                .Should()
                .BeNull();

            loteService.MaiorOperacaoCliente[operacao4.CodigoCliente].ValorDesconto
                .Should()
                .Be(operacao4.ValorFinanceiro * 0.9M);

            loteService.MaiorOperacaoCliente[operacao4.CodigoCliente].StatusBoleto
                .Should()
                .Be(OperacaoStatusBoleto.ERRO.DisplayName);

            loteService.MaiorOperacaoCliente[operacao4.CodigoCliente].Mensagem
                .Should()
                .NotBeNull();

            // Testa as Demais Operações
            loteService.DemaisOperacoes[operacao1.Id].ValorDesconto
                .Should()
                .BeNull();

            loteService.DemaisOperacoes[operacao1.Id].StatusBoleto
                .Should()
                .Be(OperacaoStatusBoleto.OK.DisplayName);

            loteService.DemaisOperacoes[operacao1.Id].Mensagem
                .Should()
                .BeNull();

            loteService.DemaisOperacoes[operacao3.Id].ValorDesconto
                .Should()
                .BeNull();

            loteService.DemaisOperacoes[operacao3.Id].StatusBoleto
                .Should()
                .Be(OperacaoStatusBoleto.ERRO.DisplayName);

            loteService.DemaisOperacoes[operacao3.Id].Mensagem
                .Should()
                .NotBeNull();

        }
    }
}
