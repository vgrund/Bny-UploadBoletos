using Bny.UploadBoletos.Domain.OperacoesAggregate;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Validations;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bny.UploadBoletos.UnitTests.DomainTests
{
    public class OperacaoValidationTests
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

        public OperacaoValidationTests()
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
        }

        [Fact]
        public void DadosParametrosValidos_EntidadeDeveEstarValida()
        {
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

            operacao.IsValid().Should().BeTrue();
            ResetDados();
        }

        [Theory]
        [InlineData("CARTEIRA CLIENTE A")]
        [InlineData("CARTEIRA CLIENTE B")]
        [InlineData("CARTEIRA CLIENTE C")]
        public void DadoUmCodigoClienteValido_NaoDeveConterMensagemErro(string codigoCliente)
        {
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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldNotHaveValidationErrorFor(x => x.CodigoCliente);
            operacao.IsValid().Should().BeTrue();

            ResetDados();
        }

        [Fact]
        public void DadoUmCodigoClienteInvalido_DeveConterMensagemErro()
        {
            codigoCliente = "CARTEIRA CLIENTE H";

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);
            
            result.ShouldHaveValidationErrorFor(x => x.CodigoCliente)
                .WithErrorMessage($"Código do cliente inválido {codigoCliente}");
            operacao.IsValid().Should().BeFalse();
            
            ResetDados();
        }

        [Fact]
        public void DadoUmCodigoClienteVazio_DeveConterMensagemErro()
        {
            codigoCliente = String.Empty;

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.CodigoCliente)
                .WithErrorMessage($"Código do cliente está vazio");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadaUmaDataVazia_DeveConterMensagemErro()
        {
            data = default(DateTime);

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Data)
                .WithErrorMessage($"Data vazia ou inválida");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadoUmTipoVazio_DeveConterMensagemErro()
        {
            tipo = String.Empty;

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Tipo)
                .WithErrorMessage($"Tipo está vazio");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadoUmTipoInvalido_DeveConterMensagemErro()
        {
            tipo = "Aluguel";

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Tipo)
                .WithErrorMessage($"Tipo da operação inválido {tipo}");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadoUmIdBolsaVazio_DeveConterMensagemErro()
        {
            idBolsa = String.Empty;

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.IdBolsa)
                .WithErrorMessage($"Id da Bolsa está vazio");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadoUmIdBolsaInvalido_DeveConterMensagemErro()
        {
            idBolsa = "NASDAQ";

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.IdBolsa)
                .WithErrorMessage($"Id da Bolsa inválido {idBolsa}");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Theory]
        [InlineData("PETR4")]
        [InlineData("VALE3")]
        public void DadoUmCodigoAtivoValido_NaoDeveConterMensagemErro(string codigoAtivo)
        {
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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldNotHaveValidationErrorFor(x => x.CodigoAtivo);
            operacao.IsValid().Should().BeTrue();

            ResetDados();
        }

        [Fact]
        public void DadoUmCorretoraVazio_DeveConterMensagemErro()
        {
            corretora = String.Empty;

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Corretora)
                .WithErrorMessage($"Corretora está vazio");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Fact]
        public void DadoUmCorretoraInvalido_DeveConterMensagemErro()
        {
            corretora = "XP";

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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Corretora)
                .WithErrorMessage($"Corretora inválida {corretora}");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void DadoQuantidadeInvalida_DeveConterMensagemErro(int quantidade)
        {
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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.Quantidade)
                .WithErrorMessage($"A quantidade não pode ser menor que zero");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 0.0M },
            new object[] { -1.0M }
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void DadoPrecoUnitarioInvalido_DeveConterMensagemErro(decimal precoUnitario)
        {
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

            var validation = new NovaOperacaoValidation();
            var result = validation.TestValidate(operacao);

            result.ShouldHaveValidationErrorFor(x => x.PrecoUnitario)
                .WithErrorMessage($"O Preço Unitário não pode ser menor que zero");
            operacao.IsValid().Should().BeFalse();

            ResetDados();
        }
    }
}
