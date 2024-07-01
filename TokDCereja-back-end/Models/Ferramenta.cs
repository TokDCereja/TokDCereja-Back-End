namespace TokDCereja_back_end.Models
{
    public class Ferramenta
    {
        public Guid Id { get; set; }

        // foreign key = chave estrangeira
        public Guid EstoqueId { get; set; }
        public Estoque? Estoque { get; set; }

        public Guid PrecificacaoId { get; set; }
        public Precificacao? Precificacao { get; set; }

        public Guid VitrineVirtualId { get; set; }
        public VitrineVirtual? VitrineVirtual { get; set; }

        public Guid TabelaNutricioalId { get; set; }
        public TabelaNutricional? TabelaNutricional { get; set; }

        public Guid HistoricoId { get; set; }
        public Historico? Historico { get; set; }

        public Guid HistoricoCaixaId { get; set; }
        public HistoricoCaixa? HistoricoCaixa { get; set; }

        public Guid CaixaId { get; set; }
        public Caixa? Caixa { get; set; }
    }
}
