namespace TokDCereja_back_end.Models
{
    public class VitrineVirtual
    {
        public Guid Id { get; set; }
        public string? NomeProduto { get; set; }
        public string? ImagemProduto { get; set; }
        public string? CategoriaProduto { get; set; }
        public string? Descricao { get; set; }
        public Guid PlanoId { get; set; }

        public Guid FerramentaId { get; set; }


        public bool IsDeleted { get; set; }  = false;
    }
}
