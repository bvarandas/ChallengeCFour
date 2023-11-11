using Microsoft.Extensions.DependencyInjection;
using ChallengeCrf.Infra.Data.UoW;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Application.Services;
using MediatR;
using ChallengeCrf.Domain.Notifications;
using ChallengeCrf.Domain.EventHandlers;
using ChallengeCrf.Domain.Events;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Infra.CrossCutting.Bus;
using ChallengeCrf.Infra.Data.Context;
using ChallengeCrf.Infra.Data.EventSourcing;
using ChallengeCrf.Infra.Data.Repository.EventSourcing;
using ChallengeCrf.Domain.CommandHandlers;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Infra.Data.Repository;

namespace ChallengeCrf.Infra.CrossCutting.Ioc;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Asp .NET HttpContext dependency
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        // Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Application
        services.AddScoped<ICashFlowService, CashFlowService>();

        // Domain - Events
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        services.AddScoped<INotificationHandler<CashFlowInsertedEvent>, RegisterEventHandler>();
        services.AddScoped<INotificationHandler<CashFlowUpdatedEvent>, RegisterEventHandler>();
        services.AddScoped<INotificationHandler<CashFlowRemovedEvent>, RegisterEventHandler>();

        // Domain - Commands
        services.AddScoped<IRequestHandler<InsertCashFlowCommand, bool>, CashFlowCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCashFlowCommand, bool>, CashFlowCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveCashFlowCommand, bool>, CashFlowCommandHandler>();

        // Infra - Data
        services.AddScoped<ICashFlowRepository, CashFlowRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<CashFlowContext>();

        // Infra - Data EventSourcing
        services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
        services.AddScoped<IEventStore, SqlEventStore>();
        services.AddScoped<EventStoreSqlContext>();

        
    }
}
