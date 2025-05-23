using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class RelatorioCidadePorHoraDto : IRelatorioModel
    {
        [Key] 
        public string? Cidade { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}

