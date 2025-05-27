using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatorioLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioCidadePorHora",
                columns: table => new
                {
                    Cidade = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioCidadePorHora", x => x.Cidade);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioFaturamentoPorPraca",
                columns: table => new
                {
                    Mes = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Praca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFatura = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioFaturamentoPorPraca", x => x.Mes);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioVeiculosPorPraca",
                columns: table => new
                {
                    Praca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TiposVeiculo = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioVeiculosPorPraca", x => x.Praca);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "RelatorioCidadePorHora");

            migrationBuilder.DropTable(
                name: "RelatorioFaturamentoPorPraca");

            migrationBuilder.DropTable(
                name: "RelatorioVeiculosPorPraca");
        }
    }
}
