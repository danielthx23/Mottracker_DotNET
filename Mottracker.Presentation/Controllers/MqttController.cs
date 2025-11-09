using Microsoft.AspNetCore.Mvc;
using Mottracker.Infrastructure.Services;

namespace Mottracker.Presentation.Controllers;

[ApiController]
[Route("api/mqtt")]
public class MqttController : ControllerBase
{
 private readonly InMemoryMqttStore _store;
 public MqttController(InMemoryMqttStore store)
 {
 _store = store;
 }

 [HttpGet("latest")] 
 public IActionResult GetLatest([FromQuery] int limit =100)
 {
 var list = _store.GetLatest(limit).Select(m => new { topic = m.Topic, payload = m.Payload, timestamp = m.Timestamp });
 return Ok(list);
 }

 [HttpGet("topic")] 
 public IActionResult GetByTopic([FromQuery] string topic, [FromQuery] int limit =100)
 {
 var list = _store.GetByTopic(topic, limit).Select(m => new { topic = m.Topic, payload = m.Payload, timestamp = m.Timestamp });
 return Ok(list);
 }

 [HttpPost("clear")] 
 public IActionResult Clear()
 {
 _store.Clear();
 return NoContent();
 }
}
