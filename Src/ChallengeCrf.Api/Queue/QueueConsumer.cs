using ChallengeCrf.Api.Hubs;
using ChallengeCrf.Domain.Extesions;
using ChallengeCrf.Domain.Interfaces;
using ChallengeCrf.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;

namespace ChallengeCrf.Api.Producer
{
    public class QueueConsumer :  IQueueConsumer
    {
        private readonly ILogger<QueueConsumer> _logger;
        private readonly QueueEventSettings _queueSettings;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<string, CashFlow> _flows;
        public QueueConsumer(
            IOptions<QueueEventSettings> queueSettings, 
            ILogger<QueueConsumer> logger,
            IServiceProvider provider)
        {
            _logger = logger;
            _queueSettings = queueSettings.Value;
            _factory = new ConnectionFactory()
            {
                HostName = _queueSettings.HostName
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _serviceProvider = provider;
            _flows = new Dictionary<string, CashFlow>();
        }

        public  async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagens Event...");
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            
            _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_queueSettings.Interval, stoppingToken);
            }
        }

        public CashFlow RegisterGetById(string registerId)
        {
            return _flows[registerId];
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var messageList = e.Body.ToArray().DeserializeFromByteArrayProtobuf<List<CashFlow>>();


                using (var scope = _serviceProvider.CreateScope())
                {
                    var hubContext = scope.ServiceProvider
                        .GetRequiredService<IHubContext<BrokerHub>>();

                    _flows.Clear();
                    messageList.ForEach(mess => {
                        _flows.TryAdd(mess.CashFlowId, mess);
                    });
                    
                    hubContext.Clients.Group("CrudMessage").SendAsync("ReceiveMessage", messageList);
                }

                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var messageList = e.Body.ToArray().DeserializeFromByteArrayProtobuf<CashFlow>();
                if (messageList is CashFlow)
                {
                    _channel.BasicAck(e.DeliveryTag, false);
                }else
                _channel.BasicNack(e.DeliveryTag, false, true);
            }
        }
    }
}
