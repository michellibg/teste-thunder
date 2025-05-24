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
    public class PassagemVeiculoDtoTests
    {
        [Fact]
        public void Deve_Falhar_Validacao_Se_Valor_For_Zero()
        {
            var passagem = new PassagemVeiculoDto
            {
                DataHora = DateTime.Now,
                Praca = "Praça 1",
                Cidade = "Apucarana",
                Estado = "PR",
                TipoVeiculo = TipoVeiculo.Moto,
                ValorPago = 0
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(passagem);
            var isValid = Validator.TryValidateObject(passagem, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "O valor pago deve ser maior que zero.");
        }

        [Fact]
        public void Deve_Criar_Passagem_Valida()
        {
            // Arrange & Act
            var passagem = new PassagemVeiculo(DateTime.Now, "Praça A", "Cidade B", 8.5m, TipoVeiculo.Carro);

            // Assert
            Assert.Equal("Praça A", passagem.Praca);
            Assert.Equal(8.5m, passagem.ValorPago);
            Assert.Equal(TipoVeiculo.Carro, passagem.TipoVeiculo);
        }

        [Fact]
        public void Nao_Deve_Criar_Passagem_Com_Valor_Negativo()
        {
            var dto = new PassagemVeiculoDto
            {
                Praca = "Praça Y",
                Cidade = "Cidade Y",
                Estado = "SP",
                ValorPago = -5,
                TipoVeiculo = TipoVeiculo.Carro,
                DataHora = DateTime.Now
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "O valor pago deve ser maior que zero.");
        }

        [Fact]
        public void Nao_Deve_Criar_Passagem_Sem_Praca()
        {
            var dto = new PassagemVeiculoDto
            {
                Praca = "",
                Cidade = "Cidade X",
                Estado = "SP",
                ValorPago = 10,
                TipoVeiculo = TipoVeiculo.Carro,
                DataHora = DateTime.Now
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "O campo 'Praça' é obrigatório.");
        }

    } 
}
