using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface IQueueProducer
{
    Task PublishMessage(CashFlow message);
    Task PublishMessage(DailyConsolidated message);
}
