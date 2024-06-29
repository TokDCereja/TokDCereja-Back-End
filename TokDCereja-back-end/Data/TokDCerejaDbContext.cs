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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Plano) 
            .WithMany(p => p.Usuarios)
            .HasForeignKey(u => u.PlanoId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
