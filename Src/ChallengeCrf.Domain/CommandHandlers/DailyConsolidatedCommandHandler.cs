
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Notifications;
using MediatR;

namespace ChallengeCrf.Domain.CommandHandlers;

public class DailyConsolidatedCommandHandler : CommandHandler,
    IRequestHandler<InsertDailyConsolidatedCommand, bool>
{
    public DailyConsolidatedCommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
    }

    public async Task<bool> Handle(InsertDailyConsolidatedCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }


        return await Task.FromResult(true);
    }
}
