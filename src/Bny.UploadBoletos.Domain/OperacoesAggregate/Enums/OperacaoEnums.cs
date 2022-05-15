using Bny.UploadBoletos.SharedKernel;

namespace Bny.UploadBoletos.Domain.OperacoesAggregate.Enums
{
    /*
     * Foi escolhida essa implementação ao invés de enums pois eles não dão 
     * um bom suporte a strings.
     * Tambem não foi utilizado o static readonly, pois essa implementação deixa 
     * aberto a mudanças em variaveis que não seriam validadas ao compilar.
     * Mais sobre isso pode ser visto nas referencias encontradas no README
     */
    public class OperacaoCodigosClientes : Enumeration
    {
        private OperacaoCodigosClientes() { }
        private OperacaoCodigosClientes(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoCodigosClientes CarteiraClienteA
            = new OperacaoCodigosClientes(0, "CARTEIRA CLIENTE A");
        public static readonly OperacaoCodigosClientes CarteiraClienteB
             = new OperacaoCodigosClientes(1, "CARTEIRA CLIENTE B");
        public static readonly OperacaoCodigosClientes CarteiraClienteC
            = new OperacaoCodigosClientes(2, "CARTEIRA CLIENTE C");
    }

    public class OperacaoTipos : Enumeration
    {
        private OperacaoTipos() { }
        private OperacaoTipos(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoTipos Compra
            = new OperacaoTipos(0, "Compra");
        public static readonly OperacaoTipos Venda
            = new OperacaoTipos(1, "Venda");
    }

    public class OperacaoBolsas : Enumeration
    {
        private OperacaoBolsas() { }
        private OperacaoBolsas(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoBolsas BVSP
            = new OperacaoBolsas(0, "BVSP");
    }

    public class OperacaoCodigosAtivos : Enumeration
    {
        private OperacaoCodigosAtivos() { }
        private OperacaoCodigosAtivos(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoCodigosAtivos PETR4
            = new OperacaoCodigosAtivos(0, "PETR4");
        public static readonly OperacaoCodigosAtivos VALE3
            = new OperacaoCodigosAtivos(1, "VALE3");
    }

    public class OperacaoCorretoras : Enumeration
    {
        private OperacaoCorretoras() { }
        private OperacaoCorretoras(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoCorretoras AGORA
            = new OperacaoCorretoras(0, "AGORA");
    }

    public class OperacaoStatusBoleto : Enumeration
    {
        private OperacaoStatusBoleto() { }
        private OperacaoStatusBoleto(int value, string displayName) : base(value, displayName) { }

        public static readonly OperacaoStatusBoleto OK
            = new OperacaoStatusBoleto(0, "OK");
        public static readonly OperacaoStatusBoleto ERRO
            = new OperacaoStatusBoleto(1, "ERRO");
    }
}
