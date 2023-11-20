using ChallengeCrf.Domain.Models;
namespace ChallengeCrf.Domain.Interfaces;
public interface IDailyConsolidatedRepository
{
    public Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date);
    public Task<IAsyncEnumerable<DailyConsolidated>> GetDailyConsolidatedListAllAsync();
    public Task AddDailyConsolidatedAsync(DailyConsolidated dailyConsolidated);
    public Task<DailyConsolidated> UpdateDailyConsolidatedAsync(DailyConsolidated dailyConsolidated);
}