namespace TokDCereja_back_end.Models
{
    public class Caixa
    {
        public Guid Id { get; set; }

        public Guid FerramentaId { get; set; }
        public float CustoFixo { get; set; }
        public float CustoUnidade { get; set; }
        public float TotalVenda { get; set; }
        public float FundoReserva { get; set; }
        public float CapitalDeGiro { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now; // puxar data atual
        public bool IsDeleted { get; set; } = false;
    }
}
