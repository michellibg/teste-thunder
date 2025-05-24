using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rebus.Handlers;
using Rebus.Messages;
using Rebus.Pipeline;
using System.Text.Json;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Infrastructure.Data;
using static Thunders.TechTest.ApiService.Constantes;
using static Thunders.TechTest.ApiService.Domain.Enum.TiposEnum;
using System.Globalization;

namespace Thunders.TechTest.ApiService.Application.Handlers
{
    public class RelatorioProcessarHandler : IHandleMessages<RelatorioProcessarMessage>
    {
        private readonly PedagioContext _context;

        public RelatorioProcessarHandler(PedagioContext context)
        {
            _context = context;
        }

        public async Task Handle(RelatorioProcessarMessage message)
        {
            var relatorio = new Relatorio();
            switch (message.NomeRelatorio)
            {
                case NomeRelatorio.HorasPorCidade:
                    relatorio = await BuscarValorTotalPorCidade(message);
                    break;

                case NomeRelatorio.PracasMaisFaturamMes:
                    relatorio = await BuscarPracaMaisFaturaMes(message);
                    break;

                case NomeRelatorio.TiposVeiculoPorPraca:
                    relatorio = await BuscarTiposVeiculoPraca(message);
                    break;

                default:
                    break;
            }

            _context.Relatorio.Add(relatorio);
            await _context.SaveChangesAsync();
        }

        private async Task<Relatorio> BuscarTiposVeiculoPraca(RelatorioProcessarMessage message)
        {
            var dados = new List<RelatorioVeiculosPorPracaDto>();
            var dataInicio = message.DataInicio == null ? DateTime.Now.AddYears(-1) : message.DataInicio;
            var dataFim = message.DataFim == null ? DateTime.Now : message.DataFim;

            if (string.IsNullOrEmpty(message.NomePraca))
            {
                message.NomePraca = string.Empty;
            }

            try
            {
                dados = await _context.PassagemVeiculo
                    .Where(p => p.Praca == message.NomePraca.Trim())
                    .GroupBy(p => p.Praca)
                    .Select(g => new RelatorioVeiculosPorPracaDto
                    {
                        Praca = g.Key,
                        TiposVeiculo = g.Select(x => x.TipoVeiculo).Distinct().Count()
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO:" + e.Message);
                throw;
            }

            return CriarRelatorio(message.NomeRelatorio, dados);
        }

        private async Task<Relatorio> BuscarPracaMaisFaturaMes(RelatorioProcessarMessage message)
        {
            var dados = new List<RelatorioFaturamentoPorPracaDto>();
            var dataInicio = message.DataInicio == null ? DateTime.Now.AddYears(-1) : message.DataInicio;
            var dataFim = message.DataFim == null ? DateTime.Now : message.DataFim;
            var qtde = message.QtdePracasMaisFaturam == 0 ? 3 : message.QtdePracasMaisFaturam;

            var sqlFormatado = string.Format(sqlFaturamentoPracas, qtde, "{0}", "{1}");

            try
            {
                dados = await _context
                     .Set<RelatorioFaturamentoPorPracaDto>()
                     .FromSqlRaw(sqlFormatado, dataInicio, dataFim)
                     .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO:" + e.Message);
                throw;
            }

            return CriarRelatorio(message.NomeRelatorio, dados);
        }

        private async Task<Relatorio> BuscarValorTotalPorCidade(RelatorioProcessarMessage message)
        {
            var dados = new List<RelatorioCidadePorHoraDto>();
            var dataInicio = message.DataInicio == null? DateTime.Now.AddYears(-1): message.DataInicio;
            var dataFim = message.DataFim == null ? DateTime.Now : message.DataFim;

            try
            {
                dados = await _context.PassagemVeiculo
                    .Where(p => p.DataHora >= dataInicio && p.DataHora <= dataFim)
                    .GroupBy(p => p.Cidade)
                    .Select(g => 
                    new RelatorioCidadePorHoraDto
                    {
                        Cidade = g.Key,
                        ValorTotal = g.Sum(x => x.ValorPago) / (g.Select(x => x.DataHora.Date).Distinct().Count() * 24.0m)
                    })
                    .OrderByDescending(x => x.ValorTotal)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO:" + e.Message);
                throw;
            }

            return CriarRelatorio(message.NomeRelatorio, dados);
        }

        private Relatorio CriarRelatorio<T>(NomeRelatorio nome, List<T> dados) 
            where T : IRelatorioModel
        {
            var nomeRelatorio = Enum.GetName(typeof(NomeRelatorio), nome);
            var relatorio = new Relatorio();
            relatorio.DataGeracao = DateTime.Now;
            relatorio.Nome = nome;
            relatorio.RelatorioJson = nomeRelatorio + JsonSerializer.Serialize(dados);
            return relatorio;
        }

    }
}

