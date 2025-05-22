using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.ApiService.Infrastructure.Data;

public class PedagioContext : DbContext
{
    public PedagioContext(DbContextOptions<PedagioContext> options) : base(options)
    {
    }

    public DbSet<PassagemVeiculo> PassagemVeiculo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PassagemVeiculo>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<PassagemVeiculo>()
            .Property(u => u.Praca)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<PassagemVeiculo>()
            .Property(u => u.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<PassagemVeiculo>()
            .Property(u => u.Estado)
            .IsRequired()
            .HasMaxLength(2);

        modelBuilder.Entity<PassagemVeiculo>()
            .Property(u => u.ValorPago)
            .HasColumnType("decimal(18,2)");
    }
}
