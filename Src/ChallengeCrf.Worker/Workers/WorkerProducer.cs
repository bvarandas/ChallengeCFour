using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeCrf.Domain.Models;
using Microsoft.Extensions.Options;
using ChallengeCrf.Domain.Extesions;
using System.Threading.Channels;
using ChallengeCrf.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ChallengeCrf.Worker.Consumer.Workers;

public class WorkerProducer :  IWorkerProducer
{
    private readonly ILogger<WorkerProducer> _logger;
    private readonly QueueEventSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private static WorkerProducer _instance = null!;

    public static WorkerProducer  _Singleton
    {
        get
        {
            return _instance;
        }

    }
    
    public WorkerProducer(IOptions<QueueEventSettings> queueSettings, ILogger<WorkerProducer> logger)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        
        try
        {
            _logger.LogInformation($"O hostname é {_queueSettings.HostName}");
            _factory = new ConnectionFactory { HostName = _queueSettings.HostName, Port=_queueSettings.Port };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_queueSettings.ExchangeService, _queueSettings.ExchangeType, true, false);
            _channel.QueueDeclare(
            queue: _queueSettings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            _channel.QueueBind(_queueSettings.QueueName, _queueSettings.ExchangeService, _queueSettings.RoutingKey);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        _instance = this;
    }

    public Task PublishMessage(CashFlow message)
    {
        try
        {
            var body = message.SerializeToByteArrayProtobuf();

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueSettings.QueueName,
                basicProperties: null,
                body: body);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
        }
        return Task.CompletedTask;
    }

    public Task PublishMessages(List<CashFlow> messageList)
    {
        try
        {
            var body = messageList.SerializeToByteArrayProtobuf();

            _channel.QueueDeclare(
            queue: _queueSettings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            _channel.BasicPublish(
                exchange: _queueSettings.ExchangeService,
                routingKey: _queueSettings.QueueName,
                basicProperties: null,
                body: body);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
        }
        return Task.CompletedTask;
    }

    public  async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }
}