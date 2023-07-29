using ChallengeCrf.Api.Producer;
using ChallengeCrf.Domain.Extesions;
using ChallengeCrf.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProtoBuf.Meta;
using ChallengeCrf.Infra.CrossCutting.Ioc;
using Microsoft.AspNetCore.SignalR;
using ChallengeCrf.Api.Hubs;
using ChallengeCrf.Queue.Worker.Configurations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

// Add services to the container.
//builder.Services
    //.AddCors()
    //.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppConfiguration(config);
builder.Services.AddSingleton<IQueueConsumer, QueueConsumer>();
builder.Services.AddSingleton<IQueueProducer, QueueProducer>();

//SignalR - Core
//builder.Services.AddSignalRCore();

//SignalR
builder.Services.AddSignalR();

// automapper
builder.Services.AddAutoMapperSetup();

// Asp .NET HttpContext dependency
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatR(cfg=> 
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); 
});

//NativeInjectorBootStrapper.RegisterServices(builder.Services);


builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builderc =>
{
    builderc
    .AllowAnyHeader()
    .AllowAnyMethod()
    //.AllowAnyOrigin()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials();
    
    //.SetIsOriginAllowed((host) => true)
    
}));

builder.Services.AddControllers();

builder.Services.AddSingleton<IQueueConsumer, QueueConsumer>();
builder.Services.AddHostedService<QueueProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(options => options.AddPolicy( { 
//    options
//    .AllowAnyOrigin()
//    .SetIsOriginAllowed((host)=> true)
//    .AllowAnyMethod()
//    .AllowAnyHeader(); 


//});



app.UseCors("CorsPolicy");

app.MapHub<BrokerHub>("/hubs/brokerhub");

//app.UseAuthorization();

app.MapControllers();

app.Run();
