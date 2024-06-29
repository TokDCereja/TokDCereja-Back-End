namespace TokDCereja_back_end.Models
{
    public class HistoricoCaixa
    {
        public Guid Id { get; set; }

        public float Valor { get; set; }
        public DateTime DataHistoricoCaixa { get; set; } = DateTime.Now; // puxar data atual
        public bool IsDeleted { get; set; } = false;
    }
}
