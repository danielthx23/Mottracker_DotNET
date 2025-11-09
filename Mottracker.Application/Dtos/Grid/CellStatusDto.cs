using System;
using System.Collections.Generic;

namespace Mottracker.Application.Dtos.Grid
{
 public class CellStatusDto
 {
 public string CellId { get; set; } = string.Empty;
 public int Count { get; set; }
 public DateTime? LastTimestamp { get; set; }
 public double? AvgSpeed { get; set; }
 public IEnumerable<string> Motos { get; set; } = Array.Empty<string>();
 }
}