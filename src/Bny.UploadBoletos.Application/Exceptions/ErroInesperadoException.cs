namespace Bny.UploadBoletos.Application.Exceptions
{
    public class ErroInesperadoException : Exception
    {
        public ErroInesperadoException(string mensagem) : base(mensagem) { }
    }
}
