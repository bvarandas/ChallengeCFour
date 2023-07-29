using ChallengeCrf.Worker;
using System.Runtime.CompilerServices;
using ChallengeCrf.Domain.Extesions;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Worker.Consumer.Workers;
using ChallengeCrf.Application.AutoMapper;
using ChallengeCrf.Queue.Worker.Configurations;
using ChallengeCrf.Infra.CrossCutting.Ioc;
using System.Reflection;
using ChallengeCrf.Application.Services;
using ChallengeCrf.Infra.Data.Repository;
using ChallengeCrf.Infra.Data.Context;
using ChallengeCrf.Infra.Data.UoW;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.CommandHandlers;
using ChallengeCrf.Domain.Commands;
using ChallengeCrf.Domain.EventHandlers;
using ChallengeCrf.Domain.Events;
using ChallengeCrf.Domain.Notifications;
using ChallengeCrf.Infra.CrossCutting.Bus;
using ChallengeCrf.Infra.Data.EventSourcing;
using ChallengeCrf.Infra.Data.Repository.EventSourcing;
using MediatR;

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.Sources.Clear();
            builder.AddConfiguration(config);

        })
        .ConfigureServices(services =>
        {
            services.AddAppConfiguration(config);

            // Domain Bus (Mediator)
            services.AddSingleton<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddSingleton<ICashFlowService, CashFlowService>();

            // Domain - Events
            services.AddSingleton<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddSingleton<INotificationHandler<CashFlowInsertedEvent>, RegisterEventHandler>();
            services.AddSingleton<INotificationHandler<CashFlowUpdatedEvent>, RegisterEventHandler>();
            services.AddSingleton<INotificationHandler<CashFlowRemovedEvent>, RegisterEventHandler>();

            // Domain - Commands
            services.AddSingleton<IRequestHandler<InsertCashFlowCommand, bool>, CashFlowCommandHandler>();
            services.AddSingleton<IRequestHandler<UpdateCashFlowCommand, bool>, CashFlowCommandHandler>();
            services.AddSingleton<IRequestHandler<RemoveCashFlowCommand, bool>, CashFlowCommandHandler>();

            // Infra - Data
            services.AddSingleton<ICashFlowRepository, CashFlowRepository>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<DbContextClass>();

            // Infra - Data EventSourcing
            services.AddSingleton<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddSingleton<IEventStore, SqlEventStore>();
            services.AddSingleton<EventStoreSqlContext>();

            //services.AddHostedService<WorkerProducer>();

            services.AddHostedService<WorkerConsumer>();

            services.AddSingleton<IWorkerProducer, WorkerProducer>();
            
            services.AddAutoMapperSetup();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            
            //NativeInjectorBootStrapper.RegisterServices(services);
        })
        .Build();
    await host.RunAsync();
}
catch(Exception ex)
{
    
}


