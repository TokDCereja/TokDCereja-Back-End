namespace TokDCereja_back_end.Models
{
    public class TabelaNutricional
    {
        public Guid Id { get; set; }
        public string? NomeReceita { get; set; }
        public float Carboidrato { get; set; }
        public float Proteina { get; set; }
        public float GorduraSaturada { get; set; }
        public float GorduraTotal { get; set; }
        public float Sodio { get; set; }
        public float ValorEnergetico { get; set; }
        public float Porcao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public float Quantidade { get; set; }
        public float Gramas { get; set; }
        public string? TipoProdutoFinal { get; set; }
        public Guid FerramentaId { get; set; }
    }
}
