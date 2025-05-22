using AutoMapper;
using k8s.KubeConfigModels;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Services.Interfaces;
using Thunders.TechTest.ApiService.Infrastructure.Data;

namespace Thunders.TechTest.ApiService.Application.Services
{
    public class PassagemVeiculoService : IPassagemVeiculoService
    {
        private readonly PedagioContext _context;
        private readonly IMapper _mapper;

        public PassagemVeiculoService(IMapper mapper, PedagioContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<int> CreatePassagensVeiculoAsync(List<PassagemVeiculoDTO> dtos)
        {
            var passagens = _mapper.Map<List<PassagemVeiculo>>(dtos);

            _context.PassagemVeiculo.AddRange(passagens);

            await _context.SaveChangesAsync();

            return 0;
        }
    }
}
