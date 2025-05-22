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
        public async Task<IActionResult> EnviarPassagemVeiculo([FromBody] List<PassagemVeiculoDTO> dtos)
        {
            var retorno = await _service.CreatePassagensVeiculoAsync(dtos);
            return Ok(retorno);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // implement if needed
            return Ok();
        }
    }
}