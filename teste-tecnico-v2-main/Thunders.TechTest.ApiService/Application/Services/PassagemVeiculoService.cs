using AutoMapper;
using k8s.KubeConfigModels;
using System.Threading;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Services.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
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

        public async Task<int> CreatePassagensVeiculoAsync(List<PassagemVeiculoDto> dtos, CancellationToken cancellationToken)
        {
            try
            {
                var passagens = _mapper.Map<List<PassagemVeiculo>>(dtos);

                _context.PassagemVeiculo.AddRange(passagens);

                await _context.SaveChangesAsync(cancellationToken);

                return 0; 
            }
            catch (Exception e)
            {
                await InserirLog(e.Message);
                return 1; 
            }
        }

        private async Task InserirLog(string message)
        {
            var log = new Log
            {
                DataRegistro = DateTime.Now,
                Mensagem = message
            };
            _context.Log.Add(log);
            await _context.SaveChangesAsync();

        }
    }
}
