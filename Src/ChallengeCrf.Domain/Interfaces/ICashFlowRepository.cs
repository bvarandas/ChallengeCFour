using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface ICashFlowRepository : IDisposable
{
    public Task<CashFlow> GetCashFlowByIDAsync(string registerId);
    public Task AddCashFlowAsync(CashFlow register);
    public Task UpdateCashFlowAsync(CashFlow register);
    public Task<IEnumerable<CashFlow>> GetAllCashFlowAsync();
    public void DeleteCashFlowAsync(string registerId);
}
