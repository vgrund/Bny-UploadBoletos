using Bny.UploadBoletos.Domain.OperacoesAggregate.Validations;
using Bny.UploadBoletos.SharedKernel;
using Bny.UploadBoletos.SharedKernel.Interfaces;

namespace Bny.UploadBoletos.Domain.OperacoesAggregate
{
    public class Operacao: BaseEntity, IAggregateRoot
    {
        /*
         * Usando um construtor com argumentos reduzidos, de acordo com o 
         * indicado pelo Sonar https://rules.sonarsource.com/csharp/RSPEC-107
         * Foi utilizado o Builder Pattern para resolver a criação deste objeto
         */
        public Operacao(string codigoCliente, DateTime data, string tipo)
        {
            CodigoCliente = codigoCliente;
            Data = data;
            Tipo = tipo;
        }

        // Construtor vazio para o EF
        public Operacao()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new NovaOperacaoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void AplicaDesconto()
        {
            ValorDesconto = ValorFinanceiro * 0.9M;
        }

        public void RetiraDesconto()
        {
            ValorDesconto = null;
        }

        public void CalculaValorFinanceiro()
        {
            ValorFinanceiro = Quantidade * PrecoUnitario;
        }

        public string CodigoCliente { get; internal set; } = string.Empty;
        public DateTime Data { get; internal set; }
        public string Tipo { get; internal set; } = string.Empty;
        public string IdBolsa { get; internal set; } = string.Empty;
        public string CodigoAtivo { get; internal set; } = string.Empty;
        public string Corretora { get; internal set; } = string.Empty;
        public int Quantidade { get; internal set; }
        public decimal PrecoUnitario { get; internal set; }

        public decimal ValorFinanceiro { get; set; }
        public decimal? ValorDesconto { get; set; }
        public string StatusBoleto { get; set; }
        public string? Mensagem { get; set; }
    }
}
