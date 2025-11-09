namespace Mottracker.Infrastructure.Models;

public class MqttOptions
{
 public string Broker { get; set; } = "localhost";
 public int Port { get; set; } =1883;
 public string ClientId { get; set; } = "mottracker-client";
 public string Topic { get; set; } = "mottracker/#";
 public string? Username { get; set; }
 public string? Password { get; set; }
 public bool UseTls { get; set; } = false;
 // Optional comma-separated topics or pattern; if provided, will be split
 public string? Topics { get; set; }
}
