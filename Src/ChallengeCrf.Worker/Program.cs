using ChallengeCrf.Domain.Extesions;
using ChallengeCrf.Queue.Worker.Workers;
using ChallengeCrf.Queue.Worker.Configurations;
using System.Reflection;
using MediatR;
using MongoFramework;
using ChallengeCrf.Application.Interfaces;
using ChallengeCrf.Infra.CrossCutting.Ioc;
using Serilog;
using Common.Logging;
using Common.Logging.Correlation;


var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.Sources.Clear();
            builder.AddConfiguration(config);

        })
        .UseSerilog(Logging.ConfigureLogger)
        .ConfigureServices(services =>
        {
            services.AddAppConfiguration(config);

            NativeInjectorBootStrapper.RegisterServices(services);

            services.AddHostedService<WorkerConsumer>();
            services.AddHostedService<WorkerDailyConsolidated>();

            services.AddSingleton<IWorkerProducer, WorkerProducer>();

            services.Configure<CashFlowSettings>(config.GetSection("CashFlowStoreDatabase"));
            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

            services.AddTransient<IMongoDbConnection>((provider) =>
            {
                var urlMongo = new MongoDB.Driver.MongoUrl("mongodb://root:example@localhost:27017/challengeCrf?authSource=admin");
                
                return MongoDbConnection.FromUrl(urlMongo);
            });
            
            services.AddAutoMapperSetup();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            
        }).Build();

    await host
    .RunAsync();