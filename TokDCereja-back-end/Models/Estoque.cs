namespace TokDCereja_back_end.Models
{
    public class Estoque
    {
        public Guid Id { get; set; }
        public Guid FerramentaId { get; set; }
        public string? Ingrediente { get; set; }
        public int QuantidadeMin { get; set; }
        public int QuantidadeMax { get; set; }
        public int QuantidadeAtual { get; set; }
        public string? Medida { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now; // puxar data atual

        public bool IsDeleted { get; set; } = false;
    }
}
