using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.OutOfBox.Queues;
using Rebus.Bus;
using Thunders.TechTest.ApiService.Domain.Enum;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.ApiService.Controllers
{
    
    public class RelatoriosController : Controller
    {
        private readonly IMessageSender _messageSender;

        public RelatoriosController(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarRelatorio([FromBody] RelatorioProcessarMessage filtros)
        {
            var msg = FiltroValido(filtros);
            
            if (msg != string.Empty)
            {
                return StatusCode(500, $"Erro : {msg}");
            };

            if (!Enum.IsDefined(typeof(NomeRelatorio), filtros.NomeRelatorio) ||
                 filtros.NomeRelatorio == NomeRelatorio.Nenhum)
            {
                return BadRequest("Nome do relatório inválido.");
            }

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            try
            {
                await _messageSender.SendLocal(filtros);
                var nomeRelatorio = Enum.GetName(typeof(NomeRelatorio), filtros.NomeRelatorio);
                return Accepted($"Relatorio {nomeRelatorio} agendado para processamento.");
            }

            catch (OperationCanceledException)
            {
                return StatusCode(408, "A operação foi cancelada por timeout.");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        private string FiltroValido(RelatorioProcessarMessage filtros)
        {
            var msg = string.Empty;

            if( filtros.DataInicio != null && filtros.DataFim != null && filtros.DataInicio > filtros.DataFim)
            {
                msg = msg + " A data de início não pode ser maior que a data de fim.";
            }   

            if (filtros.DataInicio == null || filtros.DataFim == null)
            {
                msg = msg + " A data de início e data fim são obrigatórios.";
            }

            if (filtros.NomeRelatorio == NomeRelatorio.PracasMaisFaturamMes && filtros.QtdePracasMaisFaturam <= 0)
            {
                msg = msg + " O campo 'QtdePracasMaisFaturam' é obrigatório para o relatório 'PracasMaisFaturamMes'.";
            }

            if (filtros.NomeRelatorio == NomeRelatorio.TiposVeiculoPorPraca && string.IsNullOrWhiteSpace(filtros.NomePraca))
            {
                msg = msg + " O campo 'NomePraca' é obrigatório para o relatório 'TiposVeiculoPorPraca'.";
            }

            return msg;
        }
    }
}
