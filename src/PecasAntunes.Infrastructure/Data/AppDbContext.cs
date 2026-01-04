using Microsoft.EntityFrameworkCore;
using PecasAntunes.Domain.Entities;

namespace PecasAntunes.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<AutoPeca> AutoPecas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui dizemos ao EF como a tabela deve ser criada
            modelBuilder.Entity<AutoPeca>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Marca)
                    .HasMaxLength(100);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255);

                entity.Property(e => e.Preco)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.QuantidadeEstoque)
                    .IsRequired();

                entity.Property(e => e.CriadoEm)
                    .IsRequired();
            });
        }
    }
}
