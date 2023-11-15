using ChallengeCrf.Angular.Producer;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Queue.Worker.Configurations;
using System.Reflection;

namespace ChallengeCrf.Angular.Configurations;

internal class NativeInjectorBoostrapper
{
    public static void RegisterServices(IServiceCollection services , IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<QueueCommandSettings>(config.GetSection(nameof(QueueCommandSettings)));
        services.Configure<QueueEventSettings>(config.GetSection(nameof(QueueEventSettings)));
        services.AddSingleton<IQueueConsumer, QueueConsumer>();
        services.AddSingleton<IQueueProducer, QueueProducer>();

        //SignalR
        services.AddSignalR();

        // automapper
        services.AddAutoMapperSetup();

        // Asp .NET HttpContext dependency
        services.AddHttpContextAccessor();

        // Mediator
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        services.AddCors(options => options.AddPolicy("CorsPolicy", builderc =>
        {
            builderc
            .AllowAnyHeader()
            .AllowAnyMethod()
            //.AllowAnyOrigin()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();

            //.SetIsOriginAllowed((host) => true)

        }));

        services.AddControllers();

        //services.AddSingleton<IQueueConsumer, QueueConsumer>();
        services.AddHostedService<QueueConsumer>();
        services.AddHostedService<QueueProducer>();
        
    }
}
