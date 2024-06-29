namespace TokDCereja_back_end.Models
{
    public class Agenda
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataCompromisso { get; set; } = DateTime.Now; // puxar data atual
        public string? Descricao { get; set; }

        public bool IsDeleted { get; set; } = false;  //=> Exclusão lógica. em uma aplicação que utiliza um banco de dados, você pode querer marcar um registro como "excluído" sem removê-lo fisicamente do banco de dados. Isso é útil para implementar a exclusão lógica, onde os registros são simplesmente marcados como excluídos em vez de serem removidos. Isso pode ser feito para manter um histórico dos dados ou para cumprir com requisitos de auditoria.
    }
}
