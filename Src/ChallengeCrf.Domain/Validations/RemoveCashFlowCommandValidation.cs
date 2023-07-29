using ChallengeCrf.Domain.Commands;

namespace ChallengeCrf.Domain.Validations
{
    public class RemoveCashFlowCommandValidation : CashFlowValidation<RemoveCashFlowCommand>
    {
        public RemoveCashFlowCommandValidation()
        {
            ValidateCashFlowId();
        }
    }
}
