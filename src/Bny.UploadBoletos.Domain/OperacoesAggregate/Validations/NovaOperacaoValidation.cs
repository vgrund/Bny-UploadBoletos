namespace Bny.UploadBoletos.Domain.OperacoesAggregate.Validations
{
    public class NovaOperacaoValidation : OperacaoValidation<Operacao>
    {
        public NovaOperacaoValidation()
        {
            ValidateData();
            ValidateCodigoCliente();
            ValidateTipo();
            ValidateIdBolsa();
            ValidateCodigoAtivo();
            ValidateCorretora();
            ValidateQuantidade();
            ValidatePrecoUnitario();
        }
    }
}
