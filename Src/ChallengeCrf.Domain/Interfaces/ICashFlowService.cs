using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Models;
namespace ChallengeCrf.Domain.Interfaces;
public interface ICashFlowService
{
    Task<IAsyncEnumerable<CashFlow>> GetListAllAsync();
    Task<CashFlow> GetCashFlowyIDAsync(string cashFlowId);
    Task<CashFlowCommand> AddCashFlowAsync(CashFlow register);
    Task<CashFlowCommand> UpdateCashFlowAsync(CashFlow register);
    IList<CashFlowHistoryData> GetAllHistory(int registerId);
    void RemoveCashFlowAsync(string cashFlowId);
}