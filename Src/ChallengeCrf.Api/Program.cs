using ChallengeCrf.Api.Hubs;
using ChallengeCrf.Api.Configurations;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Api.Producer;

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

app.MapPost("cashflow/",  async (CashFlow cash, IQueueProducer queueProducer, ILogger logger) => 
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
app.MapPut("cashflow/", async (CashFlow cash, IQueueProducer queueProducer, ILogger logger) => 
{
    try
    {
        cash.Action = "update";
        await queueProducer.PublishMessage(cash);

        return Results.Accepted(null, cash);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapGet("cashflow/", async (IQueueProducer queueProducer, ILogger logger) => {
    try
    {
        var cash = new CashFlow() { Action = "getall" };
        await queueProducer.PublishMessage(cash);

        return Results.Ok(null);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapGet("cashflow/{id:string}", async (IQueueProducer queueProducer, ILogger logger, string id) => {
    try
    {
        var cash = new CashFlow() { Action = "get", CashFlowId = id };
        await queueProducer.PublishMessage(cash);

        return Results.Ok(null);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.MapDelete("cashflow/{id:string}", (string id) => { });

app.UseCors("CorsPolicy");
app.MapHub<BrokerHub>("/hubs/brokerhub");
app.MapControllers();
app.Run();
