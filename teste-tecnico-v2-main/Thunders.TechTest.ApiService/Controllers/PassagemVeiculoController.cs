using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Services.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassagemVeiculoController : Controller
    {
        private readonly IPassagemVeiculoService _service;

        public PassagemVeiculoController(IPassagemVeiculoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPassagemVeiculo([FromBody] List<PassagemVeiculoDto> passagens)
        {
            var msg = DadosValidos(passagens);

            if (msg != string.Empty)
            {
                return StatusCode(500, $"Erro : {msg}");
            };

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            try
            {
                var retorno = await _service.CreatePassagensVeiculoAsync(passagens, cts.Token);
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

        private string DadosValidos(List<PassagemVeiculoDto> passagens)
        {
            if (passagens == null || !passagens.Any())
                return "A lista de passagens não pode ser nula ou vazia.";

            var mensagensErro = new List<string>();

            foreach (var passagem in passagens)
            {
                var context = new ValidationContext(passagem);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(passagem, context, results, true);

                if (!isValid)
                {
                    foreach (var validationResult in results)
                    {
                        mensagensErro.Add(validationResult.ErrorMessage ?? "Erro de validação desconhecido.");
                    }
                }
            }

            return string.Join(" ", mensagensErro);
        }
    }
}