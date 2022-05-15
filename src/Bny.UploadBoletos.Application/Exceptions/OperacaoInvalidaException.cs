using Bny.UploadBoletos.Domain.OperacoesAggregate;

namespace Bny.UploadBoletos.Application.Exceptions
{
    public class OperacaoInvalidaException: Exception
    {
        public Operacao OperacaoInvalida { get; private set; }

        public OperacaoInvalidaException(string mensagem, Operacao operacaoInvalida)
            : base(mensagem)
        {
            OperacaoInvalida = operacaoInvalida;
        }
    }
}
