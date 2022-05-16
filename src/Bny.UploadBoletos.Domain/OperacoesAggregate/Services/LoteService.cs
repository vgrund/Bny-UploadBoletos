using Bny.UploadBoletos.Domain.OperacoesAggregate.Enums;
using Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces;

namespace Bny.UploadBoletos.Domain.OperacoesAggregate.Services
{
    public sealed class LoteService : ILoteService
    {
        /*
         * A estimativa do tamanho do lote é importante para que não ocorram muitos
         * resizes da capacidade dos dicionários o que mudaria a complexidade do Add de O(1) para O(n)
         * Assumi que a quantidade de clientes diferentes deve ser bem menor que operações, considerando 1/5 das operações.
         */
        public LoteService(int tamanho)
        {
            // Programação defensiva
            _ = tamanho == 0 ? throw new ArgumentException("Tamanho não deve ser zero", nameof(tamanho)) : true;
            MaiorOperacaoCliente = new Dictionary<string, Operacao>(tamanho/5);
            DemaisOperacoes = new Dictionary<Guid, Operacao>(tamanho);
        }

        /*
         * Poderia ter utilizado HashSet no lugar dos dicionarios. Hashsets tambem
         * são performaticos e possibilitam operações de conjuntos como Union
         */
        public Dictionary<string, Operacao> MaiorOperacaoCliente { get; private set; }
        public Dictionary<Guid, Operacao> DemaisOperacoes { get; private set; }

        public void AdicionaOperacao(Operacao operacaoAdicionar)
        {
            ResolveMensagem(operacaoAdicionar);
            ResolveStatus(operacaoAdicionar);
            ResolveCalculoValorFinanceiro(operacaoAdicionar);
            ResolveDesconto(operacaoAdicionar);
        }

        private void ResolveDesconto(Operacao operacaoAdicionar)
        {
            var existeMaiorOperacaoCliente = MaiorOperacaoCliente
                .TryGetValue(operacaoAdicionar.CodigoCliente, out var maiorOperacaoExistente);

            // Talvez uma oportunidade de se utilizar Specification para reduzir os ifs
            if (!existeMaiorOperacaoCliente)
            {
                operacaoAdicionar.AplicaDesconto();
                MaiorOperacaoCliente[operacaoAdicionar.CodigoCliente] = operacaoAdicionar;
            }
            else if (operacaoAdicionar.ValorFinanceiro > maiorOperacaoExistente?.ValorFinanceiro)
            {
                operacaoAdicionar.AplicaDesconto();
                MaiorOperacaoCliente[operacaoAdicionar.CodigoCliente] = operacaoAdicionar;

                maiorOperacaoExistente.RetiraDesconto();
                DemaisOperacoes.Add(maiorOperacaoExistente.Id, maiorOperacaoExistente);
            }
            else
            {
                DemaisOperacoes.Add(operacaoAdicionar.Id, operacaoAdicionar);
            }
        }

        private void ResolveMensagem(Operacao operacaoAdicionar)
        {
            operacaoAdicionar.Mensagem = operacaoAdicionar.IsValid() ?
                operacaoAdicionar.Mensagem = null :
                operacaoAdicionar.Mensagem = String.Join(' ', operacaoAdicionar.ValidationResult.Errors);
        }

        private void ResolveStatus(Operacao operacaoAdicionar)
        {
            operacaoAdicionar.StatusBoleto = operacaoAdicionar.IsValid() ?
                 OperacaoStatusBoleto.OK.DisplayName :
                 OperacaoStatusBoleto.ERRO.DisplayName;
        }

        private void ResolveCalculoValorFinanceiro(Operacao operacaoAdicionar) => operacaoAdicionar.CalculaValorFinanceiro();
    }
}
