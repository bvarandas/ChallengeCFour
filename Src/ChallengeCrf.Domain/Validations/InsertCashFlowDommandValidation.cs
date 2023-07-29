using ChallengeCrf.Domain.Commands;
namespace ChallengeCrf.Domain.Validations;

public class InsertCashFlowDommandValidation : CashFlowValidation<InsertCashFlowCommand>
{
    public InsertCashFlowDommandValidation()
    {
        ValidateDescription();
        ValidateCashDirection();
        ValidateCashValue();
        ValidateDate();
    }
}
