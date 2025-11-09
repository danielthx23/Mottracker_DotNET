using Microsoft.ML.Data;

namespace Mottracker.Application.Models.MlModels;

public class MotoDemandInput
{
    [LoadColumn(0)]
    public float TotalMotos { get; set; }
    
    [LoadColumn(1)]
    public float DiaSemana { get; set; } // 0-6 (Domingo-Sábado)
    
    [LoadColumn(2)]
    public float Mes { get; set; } // 1-12
    
    [LoadColumn(3)]
    public float Ano { get; set; }
    
    [LoadColumn(4)]
    public float MotosDisponiveis { get; set; } // Label
}

public class MotoDemandPrediction
{
    [ColumnName("Score")]
    public float MotosDisponiveisPrevistas { get; set; }
}

