using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Application.Interfaces;

public interface IQueueProducer
{
    Task PublishMessage(CashFlow message);
    Task PublishMessage(DailyConsolidated message);
}
