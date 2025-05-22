public class PassagemVeiculo
{
    public int Id { get; set; }
    
    public DateTime DataHora { get; set; }
    
    public string? Praca { get; set; }
    
    public string? Cidade { get; set; }
    
    public string? Estado { get; set; }
    
    public decimal ValorPago { get; set; }
    
    public TipoVeiculo TipoVeiculo { get; set; }
}

public enum TipoVeiculo
{
    Moto = 1,
    Carro = 2,
    Caminhao = 3
}
