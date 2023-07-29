namespace ChallengeCrf.Domain.Events;

public  class CashFlowUpdatedEvent : Event
{
    public int RegisterId { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string Entry { get; set; }
    public DateTime Date { get; set; }

    public CashFlowUpdatedEvent(int registerId, string description, double amount, string entry, DateTime date)
    {
        RegisterId = registerId;
        Description = description;
        Entry = entry;
        Amount = amount;
        Date = date;
    }
}
