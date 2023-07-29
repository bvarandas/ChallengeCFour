using MediatR;

namespace ChallengeCrf.Domain.Events;

public abstract class Message : IRequest<bool>
{
    public string MessageType { get; protected set; } = string.Empty;
    public int AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }

}
