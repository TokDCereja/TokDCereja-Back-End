namespace TokDCereja_back_end.Models
{
    public class Precificacao
    {
        public Guid Id { get; set; }

        public Guid FerramentaId { get; set; }
        public float CustoReceita { get; set; }
        public float MaoDeObra { get; set; }

        public float CustoFixoUnitario { get; set; }

        public float CustoVariavelUnitario { get; set; }

        public float PrecoVenda { get; set; }

        public float Margemlucro { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
