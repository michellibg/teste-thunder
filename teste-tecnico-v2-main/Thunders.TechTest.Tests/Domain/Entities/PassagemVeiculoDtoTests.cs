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
        public void Deve_Criar_PassagemDto_Valida()
        {
            var passagem = new PassagemVeiculoDto
            {
                DataHora = DateTime.Now,
                Praca = "Praça 1",
                Cidade = "Apucarana",
                Estado = "PR",
                TipoVeiculo = TipoVeiculo.Moto,
                ValorPago = 10
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(passagem);
            var isValid = Validator.TryValidateObject(passagem, context, results, true);

            // Assert
            Assert.True(isValid);
            Assert.Equal("Praça 1", passagem.Praca);
            Assert.Equal(10, passagem.ValorPago);
            Assert.Equal("Apucarana", passagem.Cidade);
            Assert.Equal("PR", passagem.Estado);
            Assert.Equal(TipoVeiculo.Moto, passagem.TipoVeiculo);
        }

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
            Assert.Contains(results, r => r.ErrorMessage == "O campo 'ValorPago' deve ser maior que zero.");
        }

        [Fact]
        public void Deve_Falhar_Passagem_Com_Valor_Negativo()
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
            Assert.Contains(results, r => r.ErrorMessage == "O campo 'ValorPago' deve ser maior que zero.");
        }

        [Fact]
        public void Deve_Falhar_Passagem_Sem_Praca()
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
