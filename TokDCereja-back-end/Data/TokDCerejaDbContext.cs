using Microsoft.EntityFrameworkCore;
using TokDCereja_back_end.Models;

namespace TokDCereja_back_end.Data
{
    public class TokDCerejaDbContext : DbContext
    {
        public TokDCerejaDbContext(DbContextOptions<TokDCerejaDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Plano> Planos { get; set; }

        public DbSet<Agenda> Agendas { get; set; }

        public DbSet<Aprendizado> Aprendizados { get; set; }

        public DbSet<Caixa> Caixas { get; set; }

        public DbSet<Estoque> Estoques { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<FormaDePagamento> FormasDePagamentos { get; set; }
        public DbSet<Precificacao> Precificacoes { get; set; }

        public DbSet<TabelaNutricional> TabelasNutricionais { get; set; }
        public DbSet<VitrineVirtual> VitrinesVirtuais { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Plano) 
            .WithMany(p => p.Usuarios)
            .HasForeignKey(u => u.PlanoId)
            .OnDelete(DeleteBehavior.Restrict);

            // Configuração do relacionamento Ferramenta - Estoque
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.Estoque)            // Ferramenta possui um único Estoque
                .WithMany()                       // Estoque pode estar relacionado com várias Ferramentas
                .HasForeignKey(f => f.EstoqueId)  // Chave estrangeira em Ferramenta para EstoqueId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - Precificacao
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.Precificacao)       // Ferramenta possui uma única Precificação
                .WithMany()                       // Precificação pode estar relacionada com várias Ferramentas
                .HasForeignKey(f => f.PrecificacaoId)  // Chave estrangeira em Ferramenta para PrecificacaoId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - VitrineVirtual
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.VitrineVirtual)    // Ferramenta possui uma única Vitrine Virtual
                .WithMany()                       // Vitrine Virtual pode estar relacionada com várias Ferramentas
                .HasForeignKey(f => f.VitrineVirtualId)  // Chave estrangeira em Ferramenta para VitrineVirtualId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - TabelaNutricioal
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.TabelaNutricional)  // Ferramenta possui uma única Tabela Nutricional
                .WithMany()                       // Tabela Nutricional pode estar relacionada com várias Ferramentas
                .HasForeignKey(f => f.TabelaNutricioalId)  // Chave estrangeira em Ferramenta para TabelaNutricioalId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - Historico
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.Historico)         // Ferramenta possui um único Histórico
                .WithMany()                       // Histórico pode estar relacionado com várias Ferramentas
                .HasForeignKey(f => f.HistoricoId)  // Chave estrangeira em Ferramenta para HistoricoId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - HistoricoCaixa
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.HistoricoCaixa)    // Ferramenta possui um único Histórico do Caixa
                .WithMany()                       // Histórico do Caixa pode estar relacionado com várias Ferramentas
                .HasForeignKey(f => f.HistoricoCaixaId)  // Chave estrangeira em Ferramenta para HistoricoCaixaId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

            // Configuração do relacionamento Ferramenta - Caixa
            modelBuilder.Entity<Ferramenta>()
                .HasOne(f => f.Caixa)             // Ferramenta possui um único Caixa
                .WithMany()                       // Caixa pode estar relacionado com várias Ferramentas
                .HasForeignKey(f => f.CaixaId)    // Chave estrangeira em Ferramenta para CaixaId
                .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão se houver dependências

        }
    }
}
