using ChallengeCrf.Domain.Commands;

namespace ChallengeCrf.Domain.Validations
{
    public class UpdateCashFlowCommandValidation : CashFlowValidation<UpdateCashFlowCommand>
    {
        public UpdateCashFlowCommandValidation() 
        {
            ValidateCashFlowId();
            ValidateDescription();
            ValidateCashValue();
            ValidateCashDirection();
            ValidateDate();
        }    
    }
}