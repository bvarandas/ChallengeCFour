using ChallengeCrf.Domain.Interfaces;
using RabbitMQ.Client;
using ChallengeCrf.Domain.Extesions;
using ChallengeCrf.Domain.Models;
using Microsoft.Extensions.Options;

namespace ChallengeCrf.Api.Producer;

public class QueueProducer : BackgroundService, IQueueProducer
{
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly ILogger<QueueProducer> _logger;
    public QueueProducer(IOptions<QueueCommandSettings> queueSettings, ILogger<QueueProducer> logger)
    {

        _logger = logger;
        _queueSettings = queueSettings.Value;
        try
        {
            _factory = new ConnectionFactory { HostName = _queueSettings.HostName };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
            queue: _queueSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);
        }
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

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return Task.FromException(ex);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }
}
