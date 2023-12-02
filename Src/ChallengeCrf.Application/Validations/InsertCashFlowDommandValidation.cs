using ChallengeCrf.Application.Commands;
namespace ChallengeCrf.Application.Validations;

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
