using Bny.UploadBoletos.Domain.OperacoesAggregate;
using FluentAssertions;
using System;
using Xunit;

namespace Bny.UploadBoletos.UnitTests.DomainTests
{
    public class OperacaoTests
    {
        // Inicializando dados validos
        DateTime data;
        string tipo;
        string codigoCliente;
        string codigoAtivo;
        string idBolsa;
        string corretora;
        int quantidade;
        decimal precoUnitario;
        Operacao operacaoValida;

        public OperacaoTests()
        {
            ResetDados();
        }

        void ResetDados()
        {
            data = DateTime.Now;
            tipo = "Compra";
            codigoCliente = "CARTEIRA CLIENTE C";
            codigoAtivo = "VALE3";
            idBolsa = "BVSP";
            corretora = "AGORA";
            quantidade = 100;
            precoUnitario = 43M;

            operacaoValida = new OperacaoBuilder()
                .WithData(data)
                .WithCodigoCliente(codigoCliente)
                .WithTipo(tipo)
                .WithIdBolsa(idBolsa)
                .WithCodigoAtivo(codigoAtivo)
                .WithCorretora(corretora)
                .WithQuantidade(quantidade)
                .WithPrecoUnitario(precoUnitario)
                .Build();
        }

        [Fact]
        public void DadosParametrosValidos_DeveInicializarAtributosCorretamente()
        {
            ResetDados();

            operacaoValida.Data.Should().Be(data);
            operacaoValida.CodigoCliente.Should().Be(codigoCliente);
            operacaoValida.Tipo.Should().Be(tipo);
            operacaoValida.IdBolsa.Should().Be(idBolsa);
            operacaoValida.CodigoAtivo.Should().Be(codigoAtivo);
            operacaoValida.Corretora.Should().Be(corretora);
            operacaoValida.Quantidade.Should().Be(quantidade);
            operacaoValida.PrecoUnitario.Should().Be(precoUnitario);
            operacaoValida.IsValid().Should().BeTrue();
        }

        [Fact]
        public void DadosParametrosValidos_DeveAplicarDescontoCorretamente()
        {
            ResetDados();

            operacaoValida.AplicaDesconto();
            operacaoValida.ValorDesconto.Should().Be(operacaoValida.ValorFinanceiro * 0.9M);
        }

        [Fact]
        public void DadosParametrosValidos_DeveRetirarDescontoCorretamente()
        {
            ResetDados();

            operacaoValida.RetiraDesconto();
            operacaoValida.ValorDesconto.Should().BeNull();
        }

        [Fact]
        public void DadosParametrosValidos_DeveCalcularValorFinanceiroCorretamente()
        {
            ResetDados();

            operacaoValida.CalculaValorFinanceiro();
            operacaoValida.ValorFinanceiro.Should().Be(operacaoValida.Quantidade * operacaoValida.PrecoUnitario);
        }
    }
}
