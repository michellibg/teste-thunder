using Thunders.TechTest.ApiService.Domain.Entities;

namespace Thunders.TechTest.ApiService.Application.Messages
{
    public class RelatorioProcessarMessage
    {
        public NomeRelatorio NomeRelatorio { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? NomePraca { get; set; }
        public int QtdePracasMaisFaturam { get; set; }

    }
}

