using AutoMapper;
using Thunders.TechTest.ApiService.Application.DTOs;

namespace Thunders.TechTest.ApiService.Infrastructure.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PassagemVeiculo, PassagemVeiculoDto>().ReverseMap();
        }
    }
}


