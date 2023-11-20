using AutoMapper;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace ChallengeCrf.Application.Services;
public class DailyConsolidatedService : IDailyConsolidatedService
{
    private readonly IDailyConsolidatedRepository _dailyConsolidatedRepository;
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly ILogger<DailyConsolidatedService> _logger;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public DailyConsolidatedService(
        IMapper mapper,
        IMediatorHandler mediator,
        IDailyConsolidatedRepository registerRepository,
        ICashFlowRepository cashFlowRepository,
        ILogger<DailyConsolidatedService> logger)
    {
        _mapper = mapper;
        _dailyConsolidatedRepository = registerRepository;
        _cashFlowRepository = cashFlowRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date)
    {
        return await _dailyConsolidatedRepository.GetDailyConsolidatedByDateAsync(date);
    }

    public async Task<InsertDailyConsolidatedCommand> InsertDailyConsolidatedAsync(DailyConsolidated dailyConsolidated)
    {
        var addCommand = _mapper.Map<InsertDailyConsolidatedCommand>(dailyConsolidated);
        await _mediator.SendCommand(addCommand);

        return addCommand;
    }

    public async Task GenerateReportDailyConsolidated(CancellationToken stoppingToken)
    {
        var listCashFlow = await _cashFlowRepository.GetCashFlowByDateAsync(DateTime.Now);

        if (await listCashFlow.AnyAsync(stoppingToken))
        {
            var amountDebit = listCashFlow
                .Where(x => x.Entry == "Debito")
                .SumAsync(x => x.Amount);

            var amountCredit = listCashFlow
                .Where(x => x.Entry == "Credito")
                .SumAsync(x => x.Amount);

            var dailyConsolidated = new DailyConsolidated("", amountCredit.Result, amountDebit.Result, DateTime.Now, listCashFlow.ToEnumerable());
            var hasDailyConsolidated = await _dailyConsolidatedRepository.GetDailyConsolidatedByDateAsync(DateTime.Now);
            
            if (hasDailyConsolidated is not null)
            {
                await _dailyConsolidatedRepository.UpdateDailyConsolidatedAsync(dailyConsolidated);
            }else
            {
                await _dailyConsolidatedRepository.AddDailyConsolidatedAsync(dailyConsolidated);
            }
            _logger.LogInformation("Relatório DIário consolidado gerado com sucesso");
        }
        else
        {
            _logger.LogInformation("Não há movimento para gerar relatório de movimento consolidado");
            return;
        }
    }
}