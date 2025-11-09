using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.MlPrediction;

public class MotoDemandPredictionRequestDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O número total de motos deve ser maior que zero")]
    public int TotalMotos { get; set; }
    
    public DateTime? Data { get; set; } = DateTime.Now;
}

