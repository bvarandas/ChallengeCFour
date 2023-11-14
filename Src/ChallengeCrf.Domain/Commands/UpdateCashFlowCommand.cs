﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Commands;

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
