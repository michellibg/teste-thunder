using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    public class Relatorio
    {
        [Key]
        public int Id { get; set; } 

        public DateTime DataGeracao { get; set; }

        public NomeRelatorio Nome { get; set; }

        public string? RelatorioJson { get; set; }
    }

    public enum NomeRelatorio
    {
        HorasPorCidade = 1,
        PracasMaisFaturamMes = 2,
        TiposVeiculoPorPraca = 3
    }
}
