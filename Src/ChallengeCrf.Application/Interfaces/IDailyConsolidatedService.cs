using ChallengeCrf.Application.Commands;
using ChallengeCrf.Domain.Models;
namespace ChallengeCrf.Appplication.Interfaces;
public interface IDailyConsolidatedService
{
    Task<DailyConsolidated> GetDailyConsolidatedByDateAsync(DateTime date);
    Task<InsertDailyConsolidatedCommand> InsertDailyConsolidatedAsync(DailyConsolidated dailyConsolidated);
    Task GenerateReportDailyConsolidated(CancellationToken stoppingToken);
}
