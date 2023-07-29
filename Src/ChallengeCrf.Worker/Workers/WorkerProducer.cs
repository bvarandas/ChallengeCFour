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

    private static WorkerProducer _instance;

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
        _factory = new ConnectionFactory { HostName = _queueSettings.HostName };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _instance = this;

        _channel.QueueDeclare(
        queue: _queueSettings.QueueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);
    }

    public async Task PublishMessage(CashFlow message)
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
    }

    public async Task PublishMessages(List<CashFlow> messageList)
    {
        try
        {
            var body = messageList.SerializeToByteArrayProtobuf();

            _channel.QueueDeclare(
            queue: _queueSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueSettings.QueueName,
                basicProperties: null,
                body: body);

        }
        catch (Exception ex)
        {
            //_channel.
            _logger.LogError(ex, $"{ex.Message}");
        }
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