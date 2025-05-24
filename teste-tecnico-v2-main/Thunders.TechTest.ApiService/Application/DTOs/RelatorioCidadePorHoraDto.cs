using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class RelatorioCidadePorHoraDto : IRelatorioModel
    {
        [Key] 
        public string? Cidade { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}

