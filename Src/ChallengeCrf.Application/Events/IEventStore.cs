using ChallengeCrf.Domain.Bus;

namespace ChallengeCrf.Application.Events;
public interface IEventStore
{
    void Save<T>(T theEvent) where T : Event;
}
