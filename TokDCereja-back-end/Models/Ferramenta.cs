namespace TokDCereja_back_end.Models
{
    public class Ferramenta
    {
        public Guid Id { get; set; }

        public Guid EstoqueId { get; set; }

        public Guid PrecificacaoId { get; set; }

        public Guid VitrineVirtualId { get; set; }

        public Guid TabelaNutricioalId { get; set; }
        public Guid HistoricoId { get; set; }

        public Guid HistoricoCaixaId { get; set; }
        public Guid CaixaId { get; set; }
    }
}
