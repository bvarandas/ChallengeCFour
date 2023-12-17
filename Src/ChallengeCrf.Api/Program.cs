using ChallengeCrf.Api.Hubs;
using ChallengeCrf.Api.Configurations;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Application.ViewModel;
using ChallengeCrf.Api.Producer;
using Serilog;
using ProtoBuf.Meta;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Common.Logging;
using ChallengeCrf.Application.Interfaces;
using Common.Logging.Correlation;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using ChallengeCrf.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Host.UseSerilog(Logging.ConfigureLogger);

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

//app.MapHealthChecks("/healthz");

app.MapPost("api/cashflow/",  async (CashFlow cash, IQueueProducer queueProducer, ILogger<Program> logger ) => 
{
    try
    {
        cash.Action = UserAction.Insert;
        cash.Date = DateTime.Now;
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
        CashFlow cash = new CashFlow(cashModel.CashFlowId, cashModel.CashFlowId, cashModel.Description, cashModel.Amount, cashModel.Entry, DateTime.Now, UserAction.Update);
        cash.Id = new MongoDB.Bson.ObjectId(cashModel.CashFlowId);
        cash.cashFlowIdTemp = cashModel.CashFlowId;
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
        var cash = new CashFlow("",0,"",DateTime.Now, UserAction.GetAll) { Action = UserAction.GetAll};
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
        var cash = new CashFlow("", 0, "", DateTime.Now, UserAction.GetAll) 
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
    var cash = new CashFlow("", 0, "", DateTime.Now, UserAction.Delete)
    {
        Action = UserAction.Delete,
        CashFlowId = id,
        cashFlowIdTemp = id,
        Id = new MongoDB.Bson.ObjectId(id)
    };
    await queueProducer.PublishMessage(cash);
});

app.MapGet("api/dailyconsolidated", async ([FromQuery]string date, IQueueProducer queueProducer, ILogger<Program> logger) => 
{
    try
    {
        if (!DateTime.TryParse(date, out DateTime dateFilter))
        {
            return Results.BadRequest("Data inválida");
        }

        var dailyConsolidated = new DailyConsolidated("", 0, 0,0, dateFilter, null) { Action = UserAction.Get };

        await queueProducer.PublishMessage(dailyConsolidated);

        return Results.Ok(null);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{ex.Message}");
        return Results.BadRequest(ex);
    }
});

app.UseCors("CorsPolicy");
app.MapHub<BrokerHub>("/hubs/brokerhub");
app.MapControllers();
app.Run();
