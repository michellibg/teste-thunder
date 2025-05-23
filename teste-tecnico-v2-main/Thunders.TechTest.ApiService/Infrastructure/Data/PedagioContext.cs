using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Domain.Entities;

namespace Thunders.TechTest.ApiService.Infrastructure.Data;

public class PedagioContext : DbContext
{
    public PedagioContext(DbContextOptions<PedagioContext> options) : base(options)
    {
    }

    public DbSet<PassagemVeiculo> PassagemVeiculo { get; set; }
    public DbSet<Relatorio> Relatorio { get; set; }
    public DbSet<Log> Log { get; set; }
    public DbSet<RelatorioCidadePorHoraDto> RelatorioCidadePorHora { get; set; }
    public DbSet<RelatorioFaturamentoPorPracaDto> RelatorioFaturamentoPorPraca { get; set; }
    public DbSet<RelatorioVeiculosPorPracaDto> RelatorioVeiculosPorPraca { get; set; }

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

        modelBuilder.Entity<Relatorio>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Relatorio>()
            .Property(r => r.Nome)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Relatorio>()
            .Property(r => r.DataGeracao)
            .HasColumnType("datetime");

        modelBuilder.Entity<Relatorio>()
            .Property(r => r.RelatorioJson)
            .HasColumnType("nvarchar(max)");
    }
}
