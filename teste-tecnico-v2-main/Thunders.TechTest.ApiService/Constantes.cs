namespace Thunders.TechTest.ApiService
{
    public class Constantes
    {
        public const string sqlValorPorCidadePorHora = @"
            SELECT
                Cidade,
                SUM(ValorPago) / COUNT(DISTINCT CAST(DataHora AS DATE)) / 24 AS ValorTotal
            FROM PassagemVeiculo
            WHERE DataHora BETWEEN {0} AND {1}
            GROUP BY Cidade
            ORDER BY ValorTotal DESC";


        public const string sqlFaturamentoPracas = @"
            SELECT TOP {0}
                FORMAT(DATEFROMPARTS(YEAR(DataHora), MONTH(DataHora), 1), 'MMMM/yyyy', 'pt-BR') AS Mes,
                Praca,
                SUM(ValorPago) AS ValorFatura
            FROM PassagemVeiculo
            WHERE DataHora BETWEEN {1} AND {2}
            GROUP BY 
                Praca,
                DATEFROMPARTS(YEAR(DataHora), MONTH(DataHora), 1)
            ORDER BY
                DATEFROMPARTS(YEAR(DataHora), MONTH(DataHora), 1),
                SUM(ValorPago) DESC;
        ";
    }
}