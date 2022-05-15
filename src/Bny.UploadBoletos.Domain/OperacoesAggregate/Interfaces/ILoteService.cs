namespace Bny.UploadBoletos.Domain.OperacoesAggregate.Interfaces
{
    public interface ILoteService
    {
        Dictionary<Guid, Operacao> DemaisOperacoes
        {
            get;
        }

        Dictionary<string, Operacao> MaiorOperacaoCliente
        {
            get;
        }

        void AdicionaOperacao(Operacao operacaoAdicionar);
    }
}
