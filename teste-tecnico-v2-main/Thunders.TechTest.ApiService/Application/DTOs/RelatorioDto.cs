namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class RelatorioDto
    {
        public DateTime DataGeracao { get; set; }

        public DateTime DataHoraInicial { get; set; }

        public DateTime DataHoraFinal { get; set; }

        public string? Nome { get; set; }

        public decimal Valor { get; set; }

        public int QuantidadeRegistros { get; set; }

    }
}
