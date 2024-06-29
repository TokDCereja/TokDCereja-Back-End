namespace TokDCereja_back_end.Models
{
    public class Plano
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; } = new List<Usuario>();
    }
}
