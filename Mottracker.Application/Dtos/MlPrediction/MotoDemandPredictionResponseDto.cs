namespace Mottracker.Application.Dtos.MlPrediction;

public class MotoDemandPredictionResponseDto
{
    public int TotalMotos { get; set; }
    public DateTime Data { get; set; }
    public float MotosDisponiveisPrevistas { get; set; }
    public float PercentualDisponibilidade { get; set; }
    public string Observacao { get; set; } = string.Empty;
}

