using ChallengeCrf.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Commands;

public class RemoveCashFlowCommand : CashFlowCommand
{
    public RemoveCashFlowCommand(string cashflowId)
    {
        CashFlowId = cashflowId;
    }
    public override bool IsValid()
    {
        ValidationResult = new RemoveCashFlowCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
