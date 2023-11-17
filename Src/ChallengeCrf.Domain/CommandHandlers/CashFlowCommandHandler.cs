using Amazon.Runtime.Internal.Util;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.Events;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChallengeCrf.Domain.CommandHandlers;

public class CashFlowCommandHandler : CommandHandler,
    IRequestHandler<InsertCashFlowCommand, bool>,
    IRequestHandler<UpdateCashFlowCommand, bool>,
    IRequestHandler<RemoveCashFlowCommand, bool>
{
    private readonly ICashFlowRepository _registerRepository;
    private readonly IMediatorHandler _bus;

    public CashFlowCommandHandler(ICashFlowRepository registerRepository,
        IUnitOfWork uow,
        IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,
        ILogger<CashFlowCommandHandler> logger) : base(uow, bus, notifications)
    {
        _registerRepository = registerRepository;
        _bus = bus;
        
    }

    public async Task<bool> Handle(InsertCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new CashFlow(command.Description, command.Amount, command.Entry, command.Date, command.Action);

        _registerRepository.AddCashFlow(register);

        if (await Commit())
        {
            await _bus.RaiseEvent(new CashFlowInsertedEvent(register.CashFlowId, register.Description, register.Amount, register.Entry, register.Date , register.Action));
        }

        return await Task.FromResult(true);
    }

    public async Task<bool> Handle(UpdateCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new CashFlow(command.CashFlowId, command.CashFlowId, command.Description, command.Amount,command.Entry, command.Date, command.Action);

        await _registerRepository.UpdateCashFlowAsync(register);

        if (await Commit())
        {
            await _bus.RaiseEvent(new CashFlowUpdatedEvent(register.CashFlowId, register.Description, register.Amount, register.Entry, register.Date));
        }

        return await Task.FromResult(true);
    }

    public async Task<bool> Handle(RemoveCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        _registerRepository.DeleteCashFlowAsync(command.CashFlowId);

        if (await Commit())
        {
            await _bus.RaiseEvent(new CashFlowRemovedEvent(command.CashFlowId));
        }

        return await Task.FromResult(true);
    }
    public void Dispose()
    {
        _registerRepository.Dispose();
    }
}
