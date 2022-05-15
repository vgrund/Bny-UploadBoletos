namespace Bny.UploadBoletos.Domain.OperacoesAggregate
{
    public class OperacaoBuilder
    {
        private readonly Operacao _operacao;

        public OperacaoBuilder()
        {
            _operacao = new Operacao();
        }

        public static OperacaoBuilder operacao()
        {
            return new OperacaoBuilder();
        }

        public Operacao Build()
        {
            return _operacao;
        }

        public OperacaoBuilder WithCodigoCliente(string codigoCliente)
        {
            _operacao.CodigoCliente = codigoCliente;
            return this;
        }

        public OperacaoBuilder WithData(DateTime data)
        {
            _operacao.Data = data;
            return this;
        }

        public OperacaoBuilder WithTipo(string tipo)
        {
            _operacao.Tipo = tipo;
            return this;
        }

        public OperacaoBuilder WithIdBolsa(string idBolsa)
        {
            _operacao.IdBolsa = idBolsa;
            return this;
        }

        public OperacaoBuilder WithCodigoAtivo(string codigoAtivo)
        {
            _operacao.CodigoAtivo = codigoAtivo;
            return this;
        }

        public OperacaoBuilder WithCorretora(string corretora)
        {
            _operacao.Corretora = corretora;
            return this;
        }

        public OperacaoBuilder WithQuantidade(int quantidade)
        {
            _operacao.Quantidade = quantidade;
            return this;
        }

        public OperacaoBuilder WithPrecoUnitario(decimal precoUnitario)
        {
            _operacao.PrecoUnitario = precoUnitario;
            return this;
        }

    }
}
