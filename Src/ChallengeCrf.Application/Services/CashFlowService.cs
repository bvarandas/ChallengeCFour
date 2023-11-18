using Amazon.Runtime.Internal.Util;
using AutoMapper;
using ChallengeCrf.Application.EventSourceNormalizes;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Infra.Data.Repository.EventSourcing;
using Microsoft.Extensions.Logging;

namespace ChallengeCrf.Application.Services;

public  class CashFlowService : ICashFlowService
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<CashFlowService> _logger;
    public CashFlowService(
        IMapper mapper, 
        ICashFlowRepository registerRepository, 
        IMediatorHandler mediator, 
        IEventStoreRepository eventStoreRepository,
        ILogger<CashFlowService> logger)
    {
        _mapper = mapper;
        _cashFlowRepository = registerRepository;
        _eventStoreRepository = eventStoreRepository;
        _mediator = mediator;
        _logger = logger;
    }
    public async Task<IAsyncEnumerable<CashFlow>> GetListAllAsync()
    {
        _logger.LogInformation("Tentando ir no GetAllCashFlowAsync");
        return await _cashFlowRepository.GetAllCashFlowAsync();
    }

    public async Task<CashFlow> GetCashFlowyIDAsync(string cashFlowId)
    {
        return await _cashFlowRepository.GetCashFlowByIDAsync(cashFlowId);
    }

    public async Task<CashFlowCommand> AddCashFlowAsync(CashFlow register)
    {
        _logger.LogInformation("Tentando inserir no banco de dados");
        var addCommand = _mapper.Map<InsertCashFlowCommand>(register);
        await _mediator.SendCommand(addCommand);

        return addCommand;
    }

    public async Task<CashFlowCommand> UpdateCashFlowAsync(CashFlow register)
    {
        var updateCommand = _mapper.Map<UpdateCashFlowCommand>(register);
        await _mediator.SendCommand(updateCommand);

        return updateCommand;
    }

    public async void RemoveCashFlowAsync(string cashFlowId)
    {
        var deleteCommand = new RemoveCashFlowCommand(cashFlowId);
        await _mediator.SendCommand(deleteCommand);
    }

    public IList<CashFlowHistoryData> GetAllHistory(int registerId)
    {
        return CashFlowHistory.ToJavaScriptRegisterHistory(_eventStoreRepository.All(registerId));
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}