using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;

namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class PassagemVeiculoDto
    {
        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'DataHora' é obrigatório.")]
        public DateTime DataHora { get; set; }

        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'Praça' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo 'Praça' deve ter no máximo 100 caracteres.")]
        public string? Praca { get; set; }

        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo 'Cidade' deve ter no máximo 100 caracteres.")]
        public string? Cidade { get; set; }

        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        [StringLength(2, ErrorMessage = "O campo 'Estado' deve ter no máximo 2 caracteres.")]
        public string? Estado { get; set; }

        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'ValorPago' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo 'ValorPago' deve ser maior que zero.")]
        public decimal ValorPago { get; set; }

        [SwaggerSchema("Obrigatorio")]
        [Required(ErrorMessage = "O campo 'TipoVeiculo' é obrigatório.")]
        public TipoVeiculo TipoVeiculo { get; set; }
    }

}
