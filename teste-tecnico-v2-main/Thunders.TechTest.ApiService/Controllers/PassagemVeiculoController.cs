using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Services;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassagemVeiculoController : Controller
    {
        private readonly PassagemVeiculoService _service;

        public PassagemVeiculoController(PassagemVeiculoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPassagemVeiculo([FromBody] List<PassagemVeiculoDto> dtos)
        {

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            try
            {
                var retorno = await _service.CreatePassagensVeiculoAsync(dtos, cts.Token);
                return Ok(retorno);
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

    }
}