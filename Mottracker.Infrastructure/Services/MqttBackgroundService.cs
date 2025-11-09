using Microsoft.Extensions.Hosting;
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Protocol;
using System.Text;
using Mottracker.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Mottracker.Infrastructure.Services;

namespace Mottracker.Infrastructure.Services;

public class MqttBackgroundService : BackgroundService
{
 private readonly MqttOptions _options;
 private readonly IMqttClient _client;
 private readonly InMemoryMqttStore _store;
 private readonly IConfiguration _configuration;

 public MqttBackgroundService(MqttOptions options, IMqttClient client, InMemoryMqttStore store, IConfiguration configuration)
 {
 _options = options;
 _client = client;
 _store = store;
 _configuration = configuration;
 }

 protected override async Task ExecuteAsync(CancellationToken stoppingToken)
 {
 var builder = new MqttClientOptionsBuilder()
 .WithClientId(_options.ClientId)
 .WithTcpServer(_options.Broker, _options.Port);

 if (_options.UseTls)
 {
 builder.WithTls();
 }

 var opts = builder.Build();

 _client.ConnectedAsync += async e =>
 {
 // subscribe to configured topics
 var topics = (_options.Topics ?? _options.Topic ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
 foreach (var t in topics)
 {
 var topicFilter = new MqttTopicFilterBuilder().WithTopic(t).Build();
 await _client.SubscribeAsync(topicFilter, stoppingToken);
 }
 };

 _client.ApplicationMessageReceivedAsync += e =>
 {
 try
 {
 var topic = e.ApplicationMessage?.Topic ?? string.Empty;
 var payload = e.ApplicationMessage?.Payload == null ? string.Empty : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
 // store raw message
 _store.Add(topic, payload, DateTime.UtcNow);

 // attempt to parse JSON but do not depend on telemetry service
 // keep parsing for potential future use/logging
 try
 {
 var json = JsonDocument.Parse(payload).RootElement;
 // parsing only - no further action
 }
 catch
 {
 // ignore parsing errors
 }
 }
 catch
 {
 // ignore malformed
 }
 return Task.CompletedTask;
 };

 await _client.ConnectAsync(opts, stoppingToken);

 while (!stoppingToken.IsCancellationRequested)
 {
 await Task.Delay(1000, stoppingToken);
 }
 }
}
