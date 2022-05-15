using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bny.UploadBoletos.Application.Exceptions
{
    public class FormatoArquivoInvalidoException: Exception
    {
        public FormatoArquivoInvalidoException(string mensagem) : base(mensagem){ }
    }
}
