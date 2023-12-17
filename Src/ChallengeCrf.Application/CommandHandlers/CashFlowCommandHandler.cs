using ChallengeCrf.Application.Commands;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Events;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using FluentResults;

[assembly: InternalsVisibleTo("ChallengeCrf.Tests")]
namespace ChallengeCrf.Application.CommandHandlers;
public sealed class CashFlowCommandHandler : CommandHandler,
    IRequestHandler<InsertCashFlowCommand, Result<bool>>,
    IRequestHandler<UpdateCashFlowCommand, Result<bool>>,
    IRequestHandler<RemoveCashFlowCommand, Result<bool>>
{
    private readonly ICashFlowRepository _registerRepository;
    private readonly IMediatorHandler _mediator;

    public CashFlowCommandHandler(
        ICashFlowRepository registerRepository,
        IUnitOfWork uow,
        IMediatorHandler mediator,
        INotificationHandler<DomainNotification> notifications,
        ILogger<CashFlowCommandHandler> logger) : base(uow, mediator, notifications)
    {
        _registerRepository = registerRepository;
        _mediator = mediator;
    }

    public async Task<Result<bool>> Handle(InsertCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new CashFlow(command.Description, command.Amount, command.Entry, command.Date, command.Action);

        _registerRepository.AddCashFlow(register);

        if (await Commit(cancellationToken))
        {
            await _mediator.RaiseEvent(new CashFlowInsertedEvent(register.CashFlowId, register.Description, register.Amount, register.Entry, register.Date , register.Action));
        }

        return await Task.FromResult(true);
    }

    public async Task<Result<bool>> Handle(UpdateCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new CashFlow(command.CashFlowId, command.CashFlowId, command.Description, command.Amount,command.Entry, command.Date, command.Action);

        await _registerRepository.UpdateCashFlowAsync(register);

        if (await Commit(cancellationToken))
        {
            await _mediator.RaiseEvent(new CashFlowUpdatedEvent(register.CashFlowId, register.Description, register.Amount, register.Entry, register.Date));
        }

        return await Task.FromResult(true);
    }

    public async Task<Result<bool>> Handle(RemoveCashFlowCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        _registerRepository.DeleteCashFlowAsync(command.CashFlowId);

        if (await Commit(cancellationToken))
        {
            await _mediator.RaiseEvent(new CashFlowRemovedEvent(command.CashFlowId));
        }

        return await Task.FromResult(true);
    }
    public void Dispose()
    {
        _registerRepository.Dispose();
    }
}
