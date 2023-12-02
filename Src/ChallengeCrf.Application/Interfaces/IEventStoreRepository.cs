using ChallengeCrf.Application.Events;
namespace ChallengeCrf.Aplication.Interfaces;
public interface IEventStoreRepository : IDisposable
{
    void Store(StoredEvent theEvent);
    IList<StoredEvent> All(int aggregateId);
}
