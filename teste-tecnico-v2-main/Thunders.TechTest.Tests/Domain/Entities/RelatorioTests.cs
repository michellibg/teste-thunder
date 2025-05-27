using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Application.DTOs;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.Tests.Domain.Entities
{
    public class RelatorioTests
    {
        [Fact]
        public void Criar_RelatorioCidadePorHoraDto_Valido()
        {
            var dto = new RelatorioCidadePorHoraDto
            {
                Cidade = "São Paulo",
                ValorTotal = 100.50m
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isValid);
            // Assert
            Assert.Equal("São Paulo", dto.Cidade);
            Assert.Equal(100.50m, dto.ValorTotal);
        }
    }
}
