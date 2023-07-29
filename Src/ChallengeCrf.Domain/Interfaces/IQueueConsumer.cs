using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface IQueueConsumer
{
    Task ExecuteAsync(CancellationToken stoppingToken=default);
    CashFlow RegisterGetById(int registerId);
}
