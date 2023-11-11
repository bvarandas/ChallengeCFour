using ChallengeCrf.Domain.Models;
namespace ChallengeCrf.Domain.Interfaces;
public interface IDailyConsolidatedRepository
{
    public Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date);
    public Task<IEnumerable<DailyConsolidated>> GetDailyConsolidatedListAllAsync();
    public Task AddDailyConsolidatedAsync(DailyConsolidated register);
}