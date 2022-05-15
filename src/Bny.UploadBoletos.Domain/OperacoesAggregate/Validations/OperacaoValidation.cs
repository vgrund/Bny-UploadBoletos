using Bny.UploadBoletos.Domain.OperacoesAggregate.Enums;
using FluentValidation;

namespace Bny.UploadBoletos.Domain.OperacoesAggregate.Validations
{
    public abstract class OperacaoValidation<T> : AbstractValidator<T> where T : Operacao
    {
        protected void ValidateData()
        {
            RuleFor(c => c.Data)
                .NotEmpty().WithMessage("Data vazia ou inválida");
        }

        protected void ValidateCodigoCliente()
        {
            RuleFor(x => x.CodigoCliente)
                .NotEmpty().WithMessage("Código do cliente está vazio")
                .Must(CodigosClientesValidos)
                    .WithMessage(x => $"Código do cliente inválido {x.CodigoCliente}");

            bool CodigosClientesValidos(string codigoCliente) => 
                    string.Equals(codigoCliente, OperacaoCodigosClientes.CarteiraClienteA.DisplayName) ||
                    string.Equals(codigoCliente, OperacaoCodigosClientes.CarteiraClienteB.DisplayName) ||
                    string.Equals(codigoCliente, OperacaoCodigosClientes.CarteiraClienteC.DisplayName);
        }

        protected void ValidateTipo()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("Tipo está vazio")
                .Must(tiposValidos)
                    .WithMessage(x => $"Tipo da operação inválido {x.Tipo}");

            bool tiposValidos(string tipo) => 
                string.Equals(tipo, OperacaoTipos.Compra.DisplayName) ||
                string.Equals(tipo, OperacaoTipos.Venda.DisplayName);
        }

        protected void ValidateIdBolsa()
        {
            RuleFor(c => c.IdBolsa)
                .NotEmpty().WithMessage("Id da Bolsa está vazio")
                .Must(idsBolsaValidos)
                    .WithMessage(x => $"Id da Bolsa inválido {x.IdBolsa}");

            bool idsBolsaValidos(string idBolsa) => 
                string.Equals(idBolsa, OperacaoBolsas.BVSP.DisplayName);
        }

        protected void ValidateCodigoAtivo()
        {
            RuleFor(c => c.CodigoAtivo)
                .NotEmpty().WithMessage("Código do Ativo está vazio")
                .Must(codigoAtivoValidos)
                    .WithMessage(x => $"Código do Ativo inválido {x.CodigoAtivo}");

            bool codigoAtivoValidos(string codigoAtivo) =>
                string.Equals(codigoAtivo, OperacaoCodigosAtivos.PETR4.DisplayName) ||
                string.Equals(codigoAtivo, OperacaoCodigosAtivos.VALE3.DisplayName);
        }

        protected void ValidateCorretora()
        {
            RuleFor(c => c.Corretora)
                .NotEmpty().WithMessage("Corretora está vazia")
                .Must(corretoraValidas)
                    .WithMessage(x => $"Corretora inválida {x.Corretora}");

            bool corretoraValidas(string corretora) =>
                string.Equals(corretora, OperacaoCorretoras.AGORA.DisplayName);
        }

        protected void ValidateQuantidade()
        {
            RuleFor(c => c.Quantidade)
                .NotEmpty().WithMessage("Quantidade está vazio")
                .GreaterThan(0).WithMessage("A quantidade não pode ser menor que zero");
        }

        protected void ValidatePrecoUnitario()
        {
            RuleFor(c => c.PrecoUnitario)
                .NotEmpty().WithMessage("Preço Unitário está vazio")
                .GreaterThan(0.0M).WithMessage("O Preço Unitário não pode ser menor que zero");
        }
    }
}
