using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Events;

namespace ChallengeCrf.Domain.Bus;

public interface IMediatorHandler
{
    Task SendCommand<T>(T command) where T : Command;
    Task RaiseEvent<T>(T @event) where T : Event;
}
