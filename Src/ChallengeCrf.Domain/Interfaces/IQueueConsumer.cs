using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Domain.Interfaces;

public interface IQueueConsumer
{
    CashFlow RegisterGetById(string cashFlowId);
}
