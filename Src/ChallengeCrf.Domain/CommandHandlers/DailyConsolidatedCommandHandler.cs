
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Events;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Notifications;
using MediatR;

namespace ChallengeCrf.Domain.CommandHandlers;

public class DailyConsolidatedCommandHandler : CommandHandler,
    IRequestHandler<InsertDailyConsolidatedCommand, bool>
{
    private readonly IMediatorHandler _mediator;
    private readonly IDailyConsolidatedRepository _registerRepository;
    public DailyConsolidatedCommandHandler(
        IDailyConsolidatedRepository registerRepository,
        IUnitOfWork uow, 
        IMediatorHandler mediator, 
        INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
    {
        _registerRepository = registerRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(InsertDailyConsolidatedCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new DailyConsolidated(command., command.Amount, command.Entry, command.Date, command.Action);

        _registerRepository.AddCashFlow(register);

        if (await Commit())
        {
            await _mediator.RaiseEvent(new DailyConsolidatedInsertedEvent(register.CashFlowId, register.Description, register.Amount, register.Entry, register.Date, register.Action));
        }

        return await Task.FromResult(true);
    }
}
