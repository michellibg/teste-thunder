using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Domain.Enum;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.ApiService.Application.Messages
{
    public class RelatorioProcessarMessage : IValidatableObject
    {
        [SwaggerSchema("Tipo do relatório a ser processado. Ex: [1]HorasPorCidade, [2]PracasMaisFaturamMes, [3]TiposVeiculoPorPraca")]
        public NomeRelatorio NomeRelatorio { get; set; }

        [SwaggerSchema("Data inicial do período do relatório (opcional)")]
        public DateTime? DataInicio { get; set; }

        [SwaggerSchema("Data final do período do relatório (opcional)")]
        public DateTime? DataFim { get; set; }

        [SwaggerSchema("Obrigatório se NomeRelatorio pra TiposVeiculoPorPraca")]
        public string? NomePraca { get; set; }

        [SwaggerSchema("Obrigatório se NomeRelatorio para PracasMaisFaturamMes")]
        public int QtdePracasMaisFaturam { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NomeRelatorio == NomeRelatorio.PracasMaisFaturamMes && QtdePracasMaisFaturam <= 0)
            {
                yield return new ValidationResult("O campo 'QtdePracasMaisFaturam' é obrigatório para o relatório 'PracasMaisFaturamMes'.", new[] { nameof(QtdePracasMaisFaturam) });
            }

            if (NomeRelatorio == NomeRelatorio.TiposVeiculoPorPraca && string.IsNullOrWhiteSpace(NomePraca))
            {
                yield return new ValidationResult("O campo 'NomePraca' é obrigatório para o relatório 'TiposVeiculoPorPraca'.", new[] { nameof(NomePraca) });
            }
        }
    }
}

