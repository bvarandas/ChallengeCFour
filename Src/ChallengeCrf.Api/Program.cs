using ChallengeCrf.Api.Hubs;
using ChallengeCrf.Api.Configurations;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Application.ViewModel;
using ChallengeCrf.Api.Producer;
using Serilog;
using ProtoBuf.Meta;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

NativeInjectorBoostrapper.RegisterServices(builder.Services, config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                 .ExecuteAsync(statusCodeContext.HttpContext));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    //.WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

app.MapGet("/", (ILoggerFactory loggerFactory) => {
    var logger = loggerFactory.CreateLogger("Start");
    logger.LogInformation("Starting...");
    return "Logging at work!";
});

app.MapPost("api/cashflow/",  async (CashFlow cash, IQueueProducer queueProducer, ILogger<Program> logger ) => 
{
    try
    {
        cash.Action = "insert";
        await queueProducer.PublishMessage(cash);

        return Results.Accepted(null,cash);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});
app.MapPut("api/cashflow/", async (CashFlowViewModel cashModel, IQueueProducer queueProducer, ILogger<Program> logger) => 
{
    try
    {
        CashFlow cash = new CashFlow(cashModel.CashFlowId, cashModel.CashFlowId, cashModel.Description, cashModel.Amount, cashModel.Entry, cashModel.Date, "update");
        cash.Id = new MongoDB.Bson.ObjectId(cashModel.CashFlowId);

        await queueProducer.PublishMessage(cash);

        return Results.Accepted(null, cash);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapGet("api/cashflow/", async (IQueueProducer queueProducer, ILogger<Program> logger) => {
    try
    {
        var cash = new CashFlow("",0,"",DateTime.Now, "getall") { Action = "getall" };
        await queueProducer.PublishMessage(cash);

        return Results.Ok(null);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapGet("api/cashflow/{id}", async (IQueueProducer queueProducer, string id, ILogger<Program> logger) =>
{
    try
    {
        var cash = new CashFlow("", 0, "", DateTime.Now, "getall") 
        { 
            Action = "get", CashFlowId = id, cashFlowIdTemp= id , Id = new MongoDB.Bson.ObjectId(id) 
        };
        await queueProducer.PublishMessage(cash);

        return Results.Ok(null);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapDelete("api/cashflow/{id}", async (IQueueProducer queueProducer, string id) => {
    var cash = new CashFlow("", 0, "", DateTime.Now, "remove")
    {
        Action = "remove",
        CashFlowId = id,
        cashFlowIdTemp = id,
        Id = new MongoDB.Bson.ObjectId(id)
    };
    await queueProducer.PublishMessage(cash);
});

app.UseCors("CorsPolicy");
app.MapHub<BrokerHub>("/hubs/brokerhub");
app.MapControllers();
app.Run();
