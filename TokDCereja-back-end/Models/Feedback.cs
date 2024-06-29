namespace TokDCereja_back_end.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public string? Avaliacao { get; set; }

        public Guid UsuarioId { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public DateTime DataFeedback { get; set; } = DateTime.Now; // puxar data atual

        public bool IsDeleted { get; set; } = false;
    }
}
