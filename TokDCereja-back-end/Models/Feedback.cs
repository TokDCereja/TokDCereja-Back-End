namespace TokDCereja_back_end.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public string Avaliacao { get; set; }
        public DateTime DataFeeedback { get; set; }

        public Guid UsuarioId { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public DateTime DataFeedback { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
