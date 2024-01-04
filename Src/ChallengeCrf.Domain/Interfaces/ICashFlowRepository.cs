using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface ICashFlowRepository : IDisposable
{
    public Task<CashFlow> GetCashFlowByIDAsync(string registerId);
    public Task<IAsyncEnumerable<CashFlow>> GetCashFlowByDateAsync(DateTime date);
    public Task<bool> AddCashFlow(CashFlow register);
    public Task<bool> UpdateCashFlowAsync(CashFlow register);
    public Task<IAsyncEnumerable<CashFlow>> GetAllCashFlowAsync();
    public  Task<bool> DeleteCashFlowAsync(string registerId);
}
