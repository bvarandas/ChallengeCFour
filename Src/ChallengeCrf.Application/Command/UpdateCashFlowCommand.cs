namespace ChallengeCrf.Application.Commands;

public class UpdateCashFlowCommand : CashFlowCommand
{
    public UpdateCashFlowCommand(string cashflowId, string description, double amount, string entry, DateTime date)
    {
        CashFlowId = cashflowId;
        Description = description;
        Amount = amount;
        Entry = entry;
        Date = date;
    }

    public override bool IsValid()
    {
        //ValidationResult = new 
        return true;
    }
}
