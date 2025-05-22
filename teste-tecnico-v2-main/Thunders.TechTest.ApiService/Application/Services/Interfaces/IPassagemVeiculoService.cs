using Thunders.TechTest.ApiService.Application.DTOs;

namespace Thunders.TechTest.ApiService.Application.Services.Interfaces
{
    public interface IPassagemVeiculoService
    {
        Task<int> CreatePassagensVeiculoAsync(List<PassagemVeiculoDTO> dtos);
    }
}