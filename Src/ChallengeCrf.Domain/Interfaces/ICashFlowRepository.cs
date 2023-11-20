using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface ICashFlowRepository : IDisposable
{
    public Task<CashFlow> GetCashFlowByIDAsync(string registerId);
    public Task<IAsyncEnumerable<CashFlow>> GetCashFlowByDateAsync(DateTime date);
    public void AddCashFlow(CashFlow register);
    public Task<CashFlow> UpdateCashFlowAsync(CashFlow register);
    public Task<IAsyncEnumerable<CashFlow>> GetAllCashFlowAsync();
    public  Task DeleteCashFlowAsync(string registerId);
}
