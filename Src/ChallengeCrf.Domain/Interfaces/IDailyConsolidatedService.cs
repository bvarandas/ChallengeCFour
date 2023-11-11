using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface IDailyConsolidatedService
{
    Task<IEnumerable<DailyConsolidated>> GetListAllAsync();
    Task<DailyConsolidated> GetCashFlowyIDAsync(DateTime date);
}
