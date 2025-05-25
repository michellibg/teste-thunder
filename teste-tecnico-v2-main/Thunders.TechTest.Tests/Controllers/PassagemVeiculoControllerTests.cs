using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Application.Services.Interfaces;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.OutOfBox.Queues;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.Tests.Controllers
{
    public class PassagemVeiculoControllerTests
    {

        [Fact]
        public async Task EnviarPassagemVeiculo_RetornaOK()
        {
            // Arrange
            var passagens = new List<PassagemVeiculoDto>
            {
                new PassagemVeiculoDto {
                    DataHora = DateTime.Now,
                    TipoVeiculo = TipoVeiculo.Carro,
                    Praca = "Praça-Teste",
                    Cidade = "Vitória",
                    Estado = "ES",
                    ValorPago = 10
                },
            };

            var serviceMock = new Mock<IPassagemVeiculoService>();
            serviceMock.Setup(s => s.CreatePassagensVeiculoAsync(
                It.IsAny<List<PassagemVeiculoDto>>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var controller = new PassagemVeiculoController(serviceMock.Object);

            // Act
            var resultado = await controller.EnviarPassagemVeiculo(passagens);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(resultado);
            Assert.True(ok.StatusCode == 200);
        }

        [Fact]
        public async Task EnviarPassagemVeiculo_RetornaErroToken()
        {
            // Arrange
            var passagens = new List<PassagemVeiculoDto>
            {
                new PassagemVeiculoDto {
                    DataHora = DateTime.Now,
                    TipoVeiculo = TipoVeiculo.Carro,
                    Praca = "Praça-Teste",
                    Cidade = "Vitória",
                    Estado = "ES",
                    ValorPago = 10
                },
            };

            var serviceMock = new Mock<IPassagemVeiculoService>();
            serviceMock
                 .Setup(s => s.CreatePassagensVeiculoAsync(
                     It.IsAny<List<PassagemVeiculoDto>>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception("Simular Exception"));

            var controller = new PassagemVeiculoController(serviceMock.Object);

            // Act
            var resultado = await controller.EnviarPassagemVeiculo(passagens);

            // Assert
            var erro = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, erro.StatusCode);
            Assert.Contains("Simular Exception", erro.Value?.ToString());
        }

        [Fact]
        public async Task EnviarPassagemVeiculo_RetornaErroTimeOut()
        {
            // Arrange
            var passagens = new List<PassagemVeiculoDto>
            {
                new PassagemVeiculoDto {
                    DataHora = DateTime.Now,
                    TipoVeiculo = TipoVeiculo.Carro,
                    Praca = "Praça-Teste",
                    Cidade = "Vitória",
                    Estado = "ES",
                    ValorPago = 10
                },
            };

            var serviceMock = new Mock<IPassagemVeiculoService>();
            serviceMock
                .Setup(s => s.CreatePassagensVeiculoAsync(
                    It.IsAny<List<PassagemVeiculoDto>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            var controller = new PassagemVeiculoController(serviceMock.Object);

            // Act
            var resultado = await controller.EnviarPassagemVeiculo(passagens);

            // Assert
            var erro = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(408, erro.StatusCode);
            Assert.Contains("timeout", erro.Value?.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}

