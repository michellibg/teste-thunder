using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.OutOfBox.Queues;

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
            await _messageSender.SendLocal(filtros);
            var nomeRelatorio = Enum.GetName(typeof(NomeRelatorio), filtros.NomeRelatorio);
            return Accepted($"Relatorio {nomeRelatorio} agendado para processamento.");
        }
    }
}
