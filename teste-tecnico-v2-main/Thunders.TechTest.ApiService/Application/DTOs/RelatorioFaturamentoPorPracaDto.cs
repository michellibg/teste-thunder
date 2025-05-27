using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class RelatorioFaturamentoPorPracaDto : IRelatorioModel
    {
        [Key]
        public string? Mes { get; set; }
        public string? Praca { get; set; }
        public decimal? ValorFatura { get; set; }
    }
}

