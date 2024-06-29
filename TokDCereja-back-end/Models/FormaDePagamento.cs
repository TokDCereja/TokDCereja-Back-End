namespace TokDCereja_back_end.Models
{
    public class FormaDePagamento
    {
        public Guid Id { get; set; }

        public string NomeTitular { get; set; }
        public string CPF { get; set; }
        public char TipoPlano { get; set; }
        public float Valor { get; set; }
        public string Banco { get; set; }
        public string TipoPagamento { get; set; }

        public int NumeroParcelas { get; set; }


        public bool IsDeleted { get; set; } = false;
    }
}
