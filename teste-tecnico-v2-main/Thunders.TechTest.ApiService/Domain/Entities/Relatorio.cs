using System.ComponentModel.DataAnnotations;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

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
}

