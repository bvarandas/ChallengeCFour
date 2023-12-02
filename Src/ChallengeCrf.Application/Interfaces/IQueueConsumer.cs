using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Application.Interfaces;

public interface IQueueConsumer
{
    CashFlow RegisterGetById(string cashFlowId);
}
