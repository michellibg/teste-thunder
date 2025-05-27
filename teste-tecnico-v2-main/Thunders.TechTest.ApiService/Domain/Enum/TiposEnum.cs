using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;

namespace Thunders.TechTest.ApiService.Domain.Enum
{
    public class TiposEnum
    {

        [SwaggerSchema("1- Horas por cidade 2-Praças que mais faturam no mês 3-Tipos de veículo por praça")]
        public enum NomeRelatorio
        {
            Nenhum = 0,
            HorasPorCidade = 1,
            PracasMaisFaturamMes = 2,
            TiposVeiculoPorPraca = 3
        }

        [SwaggerSchema("1-Moto 2-Carro 3-Caminhão")]
        public enum TipoVeiculo
        {
            Moto = 1,
            Carro = 2,
            Caminhao = 3
        }                
    }
}

