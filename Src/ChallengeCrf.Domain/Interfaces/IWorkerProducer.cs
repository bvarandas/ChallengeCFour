using ChallengeCrf.Domain.Models;
namespace ChallengeCrf.Domain.Interfaces;

public interface IWorkerProducer
{
    //Task PublishMessage(CashFlow message);
    Task PublishMessages(List<CashFlow> message);
}
