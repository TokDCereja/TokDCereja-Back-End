namespace TokDCereja_back_end.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // criar new guid
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? TipoCadastro { get; set; }
        public string? TipoPlano { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now; // puxar data atual
        public bool IsDeleted { get; set; } = false; // delete lógico

        // foreign key = chave estrangeira
        public Guid PlanoId { get; set; }
        public Plano? Plano { get; set; }
    }
}
