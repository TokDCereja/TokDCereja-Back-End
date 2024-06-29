namespace TokDCereja_back_end.Models
{
    public class Aprendizado
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public bool IsDeleted { get; set; }   //=> Exclusão lógica. em uma aplicação que utiliza um banco de dados, você pode querer marcar um registro como "excluído" sem removê-lo fisicamente do banco de dados. Isso é útil para implementar a exclusão lógica, onde os registros são simplesmente marcados como excluídos em vez de serem removidos. Isso pode ser feito para manter um histórico dos dados ou para cumprir com requisitos de auditoria.

    }
}
