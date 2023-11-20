using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface IDailyConsolidatedService
{
    Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date);
    Task<InsertDailyConsolidatedCommand> InsertDailyConsolidatedAsync(DailyConsolidated dailyConsolidated);
    Task GenerateReportDailyConsolidated(CancellationToken stoppingToken);
}
