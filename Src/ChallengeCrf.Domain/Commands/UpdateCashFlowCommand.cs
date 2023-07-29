using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Commands;

public class UpdateCashFlowCommand : CashFlowCommand
{
    public UpdateCashFlowCommand(int registerId, string description, double amount, string entry, DateTime date)
    {
        RegisterId = registerId;
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
