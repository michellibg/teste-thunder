using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    public class Log
    {

        [Key]
        public int Id { get; set; }
        public DateTime DataRegistro { get; set; }
        public string? Mensagem { get; set; }

    }
}
