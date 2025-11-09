using System.Collections.Concurrent;

namespace Mottracker.Infrastructure.Services;

public class InMemoryMqttStore
{
 private readonly ConcurrentQueue<(string Topic, string Payload, DateTime Timestamp)> _messages = new();

 public void Add(string topic, string payload, DateTime timestamp)
 {
 _messages.Enqueue((topic, payload, timestamp));
 }

 public IEnumerable<(string Topic, string Payload, DateTime Timestamp)> GetLatest(int limit =100)
 {
 return _messages.Reverse().Take(limit);
 }

 public IEnumerable<(string Topic, string Payload, DateTime Timestamp)> GetByTopic(string topic, int limit =100)
 {
 return _messages.Reverse().Where(m => m.Topic == topic).Take(limit);
 }

 public void Clear() => _messages.Clear();
}
