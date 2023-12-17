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
    private readonly Mock<INotificationHandler<DomainNotification>> _notificationsMock;

    private readonly CashFlowCommandHandler _handler;

    public CashFlowCommandHandlers()
    {
        _cashFlowRepositoryMock = new();
        _unitOfWorkMock = new();
        _mediatorHandlerMock = new();
        _loggerMock = new();
        _notificationsMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenCashFlowIsNotUnique()
    {
        // Arrange
        var command = new InsertCashFlowCommand("Teste de fluxo de caixa", 500.00, CashFlowEntry.Debit, DateTime.Now);

        //_cashFlowRepositoryMock.Setup(x=>x.)

        var handler = new CashFlowCommandHandler(_cashFlowRepositoryMock.Object, _unitOfWorkMock.Object, _mediatorHandlerMock.Object, _notificationsMock.Object, _loggerMock.Object);
        // Action
        Result<bool> result = await handler.Handle(command, default);
                
        // Assert
        //result..Should().BeTrue();
        //result.Error.Should().Be(
    }
}
