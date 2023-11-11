using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Application.Services;
public class DailyConsolidatedService : IDailyConsolidatedService
{
    private readonly IDailyConsolidatedRepository _dailyConsolidatedRepository;

    public DailyConsolidatedService(IDailyConsolidatedRepository registerRepository)
    {
        _dailyConsolidatedRepository = registerRepository;
    }

    public async Task<IEnumerable<DailyConsolidated>> GetListAllAsync()
    {
        return await _dailyConsolidatedRepository.GetDailyConsolidatedListAllAsync();
    }

    public async Task<DailyConsolidated> GetCashFlowyIDAsync(DateTime date)
    {
        return await _dailyConsolidatedRepository.GetDailyConsolidatedByDateAsync(date);
    }
}