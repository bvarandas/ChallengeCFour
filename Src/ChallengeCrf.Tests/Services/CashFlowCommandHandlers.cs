using Xunit;
using Moq;
using ChallengeCrf.Application.Commands;
using ChallengeCrf.Domain.Constants;
using ChallengeCrf.Application.CommandHandlers;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Bus;
using Microsoft.Extensions.Logging;
using MediatR;
using ChallengeCrf.Domain.Notifications;
using FluentAssertions;
using FluentResults;

namespace ChallengeCrf.Tests.Services;

public class CashFlowCommandHandlers
{
    private readonly Mock<ICashFlowRepository> _cashFlowRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
    private readonly Mock<ILogger<CashFlowCommandHandler>> _loggerMock;
    private readonly DomainNotificationHandler _notifications;

    private readonly CashFlowCommandHandler _handler;

    public CashFlowCommandHandlers(INotificationHandler<DomainNotification> notifications)
    {
        _cashFlowRepositoryMock = new();
        _unitOfWorkMock = new();
        _mediatorHandlerMock = new();
        _loggerMock = new();
        _notifications = (DomainNotificationHandler)notifications;
    }

    

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_When_Insert_CashFlowIsNegative()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", -500.00, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notifications, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        
    }

    [Fact]
    public async Task Handle_Insert__Should_ReturnFailureResult_When_Insert_CashFlowIsZero()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", 0, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notifications, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        //result.Error.Should().Be(
    }

    [Fact]
    public async Task Handle_Insert__Should_ReturnFailureResult_WhenCashFlowWithoutDescription()
    {
        // Arrange
        var command = new InsertCashFlowCommand(string.Empty, 0, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notifications, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        //result.Error.Should().Be(
    }


    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_When_Insert_CashFlowIsPositivo()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", 500.00, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        // command.IsValid();

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mediatorHandlerMock.Object,
            _notifications,
            _loggerMock.Object);

        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        //result.Errors.Should()..Be(true);
        //result.Error.Should().Be(
    }

    [Fact]
    public async Task Handle_Insert__Should_ReturnSuccessResult_When_Insert_CashFlowGreaterThenZero()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", 10, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notifications, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        //result.Error.Should().Be(
    }

    [Fact]
    public async Task Handle_Insert__Should_ReturnSuccessResult_WhenCashFlowWithoutDescription()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", 0, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notifications, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        //result.Error.Should().Be(
    }
}
