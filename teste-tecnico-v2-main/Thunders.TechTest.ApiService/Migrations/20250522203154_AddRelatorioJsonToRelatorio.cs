using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatorioJsonToRelatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraFinal",
                table: "Relatorio");

            migrationBuilder.DropColumn(
                name: "DataHoraInicial",
                table: "Relatorio");

            migrationBuilder.DropColumn(
                name: "QuantidadeRegistros",
                table: "Relatorio");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Relatorio");

            migrationBuilder.AlterColumn<int>(
                name: "Nome",
                table: "Relatorio",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "RelatorioJson",
                table: "Relatorio",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatorioJson",
                table: "Relatorio");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Relatorio",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFinal",
                table: "Relatorio",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraInicial",
                table: "Relatorio",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeRegistros",
                table: "Relatorio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Relatorio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
