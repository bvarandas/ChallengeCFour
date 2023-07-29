using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeCrf.Domain.Models;
using ChallengeCrf.Domain.Extesions;
using Microsoft.Extensions.Options;
using ChallengeCrf.Domain.CommandHandlers;
using ChallengeCrf.Domain.Bus;
using ChallengeCrf.Domain.Commands;
using Microsoft.EntityFrameworkCore.Internal;
using ChallengeCrf.Domain.Interfaces;
using System.Collections.Concurrent;

namespace ChallengeCrf.Worker.Consumer.Workers;

public class WorkerConsumer : BackgroundService, IWorkerConsumer
{
    private readonly IWorkerProducer _workerProducer;
    private readonly ICashFlowService _flowService;
    private readonly ILogger<WorkerConsumer> _logger;
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ConcurrentQueue<CashFlow> _queueRegister;
    public WorkerConsumer(IOptions<QueueCommandSettings> queueSettings, 
        ILogger<WorkerConsumer> logger, ICashFlowService registerService,
        IWorkerProducer workerProducer)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _factory = new ConnectionFactory()
        {
            HostName = _queueSettings.HostName
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _flowService = registerService;
        _workerProducer = workerProducer;
        _queueRegister = new ConcurrentQueue<CashFlow>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Aguardando mensagens Command...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;

        _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);


        while (!stoppingToken.IsCancellationRequested)
        {
            //DequeueRegisterStatus();
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }

    private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var message = e.Body.ToArray().DeserializeFromByteArrayProtobuf<CashFlow>();
            _logger.LogInformation($"{message.Description} | {message.Amount}| {message.Entry} | {message.Date}");

            _queueRegister.Enqueue(message);

            switch(message.Action)
            {
                case "insert":
                    await _flowService.AddCashFlowAsync(message);
                    break;

                case "update":
                    await _flowService.UpdateCashFlowAsync(message);
                    break;

                case "remove":
                    _flowService.RemoveCashFlowAsync(message.CashFlowId);
                    break;

                case "getall":
                    var registerlist = await _flowService.GetListAllAsync();
                    await WorkerProducer._Singleton.PublishMessages(registerlist.ToList());
                    break;

                case "get":
                    var register = await _flowService.GetCashFlowyIDAsync(message.CashFlowId);
                    var list = new List<CashFlow>();
                    list.Add(register);
                    await WorkerProducer._Singleton.PublishMessages(list);
                    break;

            }

            _channel.BasicAck(e.DeliveryTag, false);

        }catch (Exception ex) 
        {
            _channel.BasicNack(e.DeliveryTag, false, true);
        }
    }

    //private async void DequeueRegisterStatus()
    //{
    //    try
    //    {
    //        if (_queueRegister.TryDequeue(out CashFlow message))
    //        {
    //            switch(message.Status.ToLower())
    //            {
    //                case "com defeito":
    //                    message.Status = "Em conserto";
    //                    _queueRegister.Enqueue(message);
    //                    await _registerService.UpdateRegisterAsync(message);

    //                    break;

    //                case "em conserto":
    //                    message.Status = "Reparado";
    //                    _queueRegister.Enqueue(message);
    //                    await _registerService.UpdateRegisterAsync(message);

    //                    break;

    //                case "reparado":
    //                    message.Status = "Devolvido";
    //                    _queueRegister.Enqueue(message);
    //                    await _registerService.UpdateRegisterAsync(message);
    //                    break;

    //                case "devolvido":
    //                    message.Status = "Faturado";
    //                    _queueRegister.Enqueue(message);
    //                    await _registerService.UpdateRegisterAsync(message);
    //                    break;

    //                case "faturado":
    //                    break;

    //            }
                
    //        }
    //        var registerlist = await _registerService.GetListAllAsync();
    //        await WorkerProducer._Singleton.PublishMessages(registerlist.ToList());
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError("DequeueRegisterStatus", ex.Message);
            
    //    }
    //}
}