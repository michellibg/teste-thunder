using AutoMapper;
using Moq;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Services;
using Thunders.TechTest.ApiService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;


namespace Thunders.TechTest.Tests.Services
{
    public class PassagemVeiculoServiceTests
    {
        private readonly IMapper _mapper;

        public PassagemVeiculoServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PassagemVeiculoDto, PassagemVeiculo>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreatePassagensVeiculoAsync_Deve_Salvar_Passagens_E_Retornar_0()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PedagioContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PedagioContext(options);

            var service = new PassagemVeiculoService(_mapper, context);

            var dtos = new List<PassagemVeiculoDto>
        {
            new PassagemVeiculoDto
            {
                DataHora = DateTime.Now,
                Cidade = "Cidade A",
                Estado = "SP",
                Praca = "Praça 1",
                TipoVeiculo = TipoVeiculo.Carro,
                ValorPago = 10.5m
            }
        };

            // Act
            var result = await service.CreatePassagensVeiculoAsync(dtos, CancellationToken.None);

            // Assert
            Assert.Equal(0, result);
            Assert.Single(context.PassagemVeiculo);
        }

        [Fact]
        public async Task CreatePassagensVeiculoAsync_Deve_Inserir_Log_Em_Caso_De_Erro()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<PassagemVeiculo>>(It.IsAny<List<PassagemVeiculoDto>>()))
                      .Throws(new Exception("Erro ao mapear"));

            var options = new DbContextOptionsBuilder<PedagioContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PedagioContext(options);

            var service = new PassagemVeiculoService(mockMapper.Object, context);

            var dtos = new List<PassagemVeiculoDto>
        {
            new PassagemVeiculoDto
            {
                DataHora = DateTime.Now,
                Cidade = "Cidade B",
                Estado = "RJ",
                Praca = "Praça 2",
                TipoVeiculo = TipoVeiculo.Caminhao,
                ValorPago = 20.0m
            }
        };

            // Act
            var result = await service.CreatePassagensVeiculoAsync(dtos, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            Assert.Single(context.Log);
            Assert.Contains("Erro ao mapear", context.Log.First().Mensagem);
        }
    }


}

