namespace ChallengeCrf.Domain.Events;

public class CashFlowRemovedEvent : Event
{
    public int CashFlowId { get; set; }
    public CashFlowRemovedEvent(int cashFlowId)
    {
        CashFlowId = cashFlowId;
    }
}
