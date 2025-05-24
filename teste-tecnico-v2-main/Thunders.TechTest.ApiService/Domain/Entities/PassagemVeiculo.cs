using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;
public class PassagemVeiculo
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string? Praca { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public decimal ValorPago { get; set; }
    public TipoVeiculo TipoVeiculo { get; set; }

    public PassagemVeiculo(DateTime dataHora, string praca, string cidade, decimal valorPago, TipoVeiculo tipoVeiculo)
    {
        if (string.IsNullOrWhiteSpace(praca)) throw new ArgumentException("Praca é obrigatória.");
        if (valorPago <= 0) throw new ArgumentException("Valor pago deve ser maior que zero.");

        DataHora = dataHora;
        Praca = praca;
        Cidade = cidade;
        ValorPago = valorPago;
        TipoVeiculo = tipoVeiculo;
    }
}
