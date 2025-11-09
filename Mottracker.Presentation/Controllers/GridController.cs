using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Dtos.Grid;
using Mottracker.Infrastructure.Services;
using System.Text.Json;

namespace Mottracker.Presentation.Controllers;

[ApiController]
[Route("api/grid")]
public class GridController : ControllerBase
{
 private readonly InMemoryMqttStore _store;
 private readonly IConfiguration _configuration;

 public GridController(InMemoryMqttStore store, IConfiguration configuration)
 {
 _store = store;
 _configuration = configuration;
 }

 [HttpGet("{patioId}/cells")]
 public IActionResult GetCells(string patioId, [FromQuery] DateTime? since)
 {
 // read latest messages and try to extract telemetry-like payloads
 var msgs = _store.GetLatest(1000);
 var list = new List<(string MotoId, string CellId, double? Speed, DateTime Timestamp)>();

 double cellSize =1.0;
 var cellSizeStr = _configuration["Grid:CellSize"];
 if (!string.IsNullOrEmpty(cellSizeStr) && double.TryParse(cellSizeStr, out var cs)) cellSize = cs;

 foreach (var m in msgs)
 {
 if (since.HasValue && m.Timestamp < since.Value) continue;
 if (!string.IsNullOrEmpty(patioId))
 {
 // optional: ignore patio filtering if not present in payload
 }

 try
 {
 if (m.Payload.StartsWith("{") )
 {
 var json = JsonDocument.Parse(m.Payload).RootElement;
 string motoId = json.TryGetProperty("motoId", out var mid) ? mid.GetString() ?? string.Empty : string.Empty;
 double? x = null, y = null; double? speed = null;
 if (json.TryGetProperty("x", out var xp) && xp.TryGetDouble(out var xd)) x = xd;
 if (json.TryGetProperty("y", out var yp) && yp.TryGetDouble(out var yd)) y = yd;
 if (json.TryGetProperty("lat", out var latp) && latp.TryGetDouble(out var latd) && json.TryGetProperty("lng", out var lngp) && lngp.TryGetDouble(out var lngd))
 {
 x = latd; y = lngd; // treat lat/lng as x/y for grid
 }
 if (json.TryGetProperty("speed", out var sp) && sp.TryGetDouble(out var sd)) speed = sd;

 if (x.HasValue && y.HasValue)
 {
 var col = (int)Math.Floor(x.Value / cellSize);
 var row = (int)Math.Floor(y.Value / cellSize);
 var cellId = $"r{row}_c{col}";
 list.Add((motoId, cellId, speed, m.Timestamp));
 }
 }
 }
 catch
 {
 // ignore parse errors
 }
 }

 var grouped = list.GroupBy(i => i.CellId ?? "unknown")
 .Select(g => new CellStatusDto
 {
 CellId = g.Key ?? string.Empty,
 Count = g.Count(),
 LastTimestamp = g.Max(x => x.Timestamp),
 AvgSpeed = g.Where(x => x.Speed.HasValue).Any() ? g.Where(x => x.Speed.HasValue).Average(x => x.Speed!.Value) : (double?)null,
 Motos = g.Select(x => x.MotoId).Where(s => !string.IsNullOrEmpty(s)).Distinct()
 });

 return Ok(grouped);
 }

 [HttpGet("{patioId}/motos/latest")]
 public IActionResult GetLatestMotos(string patioId)
 {
 var msgs = _store.GetLatest(1000);
 var latestByMoto = new Dictionary<string, (double? X, double? Y, double? Speed, DateTime Timestamp)>();

 foreach (var m in msgs)
 {
 try
 {
 if (!m.Payload.StartsWith("{")) continue;
 var json = JsonDocument.Parse(m.Payload).RootElement;
 var motoId = json.TryGetProperty("motoId", out var mid) ? mid.GetString() ?? string.Empty : string.Empty;
 if (string.IsNullOrEmpty(motoId)) continue;
 double? x = null, y = null, speed = null;
 if (json.TryGetProperty("x", out var xp) && xp.TryGetDouble(out var xd)) x = xd;
 if (json.TryGetProperty("y", out var yp) && yp.TryGetDouble(out var yd)) y = yd;
 if (json.TryGetProperty("lat", out var latp) && latp.TryGetDouble(out var latd) && json.TryGetProperty("lng", out var lngp) && lngp.TryGetDouble(out var lngd))
 {
 x = latd; y = lngd;
 }
 if (json.TryGetProperty("speed", out var sp) && sp.TryGetDouble(out var sd)) speed = sd;

 if (!latestByMoto.TryGetValue(motoId, out var current) || m.Timestamp > current.Timestamp)
 {
 latestByMoto[motoId] = (x, y, speed, m.Timestamp);
 }
 }
 catch
 {
 // ignore
 }
 }

 var result = latestByMoto.Select(kv => new {
 motoId = kv.Key,
 x = kv.Value.X,
 y = kv.Value.Y,
 speed = kv.Value.Speed,
 timestamp = kv.Value.Timestamp
 });

 return Ok(result);
 }
}
