using AutoMapper;
using ChallengeCrf.Application.EventSourceNormalizes;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Infra.Data.Repository.EventSourcing;

namespace ChallengeCrf.Application.Services;

public  class CashFlowService : ICashFlowService
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _bus;
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IEventStoreRepository _eventStoreRepository;
    public CashFlowService(
        IMapper mapper, 
        ICashFlowRepository registerRepository, 
        IMediatorHandler bus, 
        IEventStoreRepository eventStoreRepository)
    {
        _mapper = mapper;
        _cashFlowRepository = registerRepository;
        _eventStoreRepository = eventStoreRepository;
        _bus = bus;
    }
    public async Task<IEnumerable<CashFlow>> GetListAllAsync()
    {
        return await _cashFlowRepository.GetAllCashFlowAsync();
    }

    public async Task<CashFlow> GetCashFlowyIDAsync(string cashFlowId)
    {
        return await _cashFlowRepository.GetCashFlowByIDAsync(cashFlowId);
    }

    public async Task<CashFlowCommand> AddCashFlowAsync(CashFlow register)
    {
        var addCommand = _mapper.Map<InsertCashFlowCommand>(register);
        await _bus.SendCommand(addCommand);

        return addCommand;
    }

    public async Task<CashFlowCommand> UpdateCashFlowAsync(CashFlow register)
    {
        var updateCommand = _mapper.Map<UpdateCashFlowCommand>(register);
        await _bus.SendCommand(updateCommand);

        return updateCommand;
    }

    public async void RemoveCashFlowAsync(string cashFlowId)
    {
        var deleteCommand = new RemoveCashFlowCommand(cashFlowId);
        await _bus.SendCommand(deleteCommand);
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