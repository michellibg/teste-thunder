using Microsoft.AspNetCore.Mvc;
using Moq;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.OutOfBox.Queues;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Domain.Enum;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.Tests.Controllers
{
    public class RelatoriosControllerTests
    {
        [Fact]
        public async Task ProcessarRelatorio_HorasPorCidade_DeveRetornarAccepted()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.HorasPorCidade,
                DataInicio = DateTime.Now.AddDays(-30),
                DataFim = DateTime.Now,
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var acceptedResult = Assert.IsType<AcceptedResult>(resultado);
            Assert.Contains("Relatorio HorasPorCidade agendado para processamento.", acceptedResult.Location);
        }

        [Fact]
        public async Task ProcessarRelatorio_TiposVeiculoPorPraca_DeveRetornarAccepted()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.TiposVeiculoPorPraca,
                DataInicio = DateTime.Now.AddDays(-30),
                DataFim = DateTime.Now,
                NomePraca = "Praca 1"
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var acceptedResult = Assert.IsType<AcceptedResult>(resultado);
            Assert.Contains("Relatorio TiposVeiculoPorPraca agendado para processamento.", acceptedResult.Location);
        }

        [Fact]
        public async Task ProcessarRelatorio_DeveRetornarAccepted()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.PracasMaisFaturamMes,
                DataInicio = DateTime.Now.AddDays(-30),
                DataFim = DateTime.Now,
                QtdePracasMaisFaturam = 5
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var acceptedResult = Assert.IsType<AcceptedResult>(resultado);
            Assert.Contains("Relatorio PracasMaisFaturamMes agendado para processamento.", acceptedResult.Location);
        }

        [Fact]
        public async Task ProcessarRelatorio_PracasMaisFaturamMes_DeveRetornarErro()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.PracasMaisFaturamMes,                
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var erro = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, erro.StatusCode);
            Assert.Contains("A data de início e data fim são obrigatórios. O campo 'QtdePracasMaisFaturam' é obrigatório para o relatório 'PracasMaisFaturamMes'.", erro.Value?.ToString());
        }

        [Fact]
        public async Task ProcessarRelatorio_HorasPorCidade_DeveRetornarErro()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.HorasPorCidade,
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var erro = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, erro.StatusCode);
            Assert.Contains("A data de início e data fim são obrigatórios.", erro.Value?.ToString());
        }

        [Fact]
        public async Task ProcessarRelatorio_TiposVeiculoPorPraca_DeveRetornarErro()
        {
            // Arrange
            var messageSenderMock = new Mock<IMessageSender>();
            var controller = new RelatoriosController(messageSenderMock.Object);

            var filtros = new RelatorioProcessarMessage
            {
                NomeRelatorio = NomeRelatorio.TiposVeiculoPorPraca,
            };

            // Act
            var resultado = await controller.ProcessarRelatorio(filtros);

            // Assert
            var erro = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, erro.StatusCode);
            Assert.Contains("A data de início e data fim são obrigatórios. O campo 'NomePraca' é obrigatório para o relatório 'TiposVeiculoPorPraca'.", erro.Value?.ToString());
        }
    }
}




