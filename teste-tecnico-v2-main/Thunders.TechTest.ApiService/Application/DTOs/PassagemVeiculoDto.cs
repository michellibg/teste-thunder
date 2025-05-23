namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class PassagemVeiculoDto
    {
        public DateTime DataHora { get; set; }

        public string? Praca { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }

        public decimal ValorPago { get; set; }

        public TipoVeiculo TipoVeiculo { get; set; }
    }
}
