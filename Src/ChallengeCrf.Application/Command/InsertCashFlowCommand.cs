namespace ChallengeCrf.Application.Commands;

public class InsertCashFlowCommand : CashFlowCommand
{
    public InsertCashFlowCommand(string description, double amount, string entry,  DateTime date )
    {
        Description = description;
        Amount = amount;
        Entry  = entry;
        Date = date;
        
    }

    public override bool IsValid()
    {
        return true;
    }
}
