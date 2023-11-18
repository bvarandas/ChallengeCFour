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
    private readonly ILogger<DailyConsolidatedService> _logger;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public DailyConsolidatedService(
        IMapper mapper,
        IMediatorHandler mediator,
        IDailyConsolidatedRepository registerRepository,
        ILogger<DailyConsolidatedService> logger)
    {
        _mapper = mapper;
        _dailyConsolidatedRepository = registerRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IEnumerable<DailyConsolidated>> GetListAllAsync()
    {
        return await _dailyConsolidatedRepository.GetDailyConsolidatedListAllAsync();
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
}