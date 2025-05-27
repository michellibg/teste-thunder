using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class RelatorioVeiculosPorPracaDto : IRelatorioModel
    {
        [Key] 
        public string? Praca { get; set; }
        public decimal? TiposVeiculo { get; set; }
    }
}

