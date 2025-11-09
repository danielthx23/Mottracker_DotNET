using Microsoft.ML;
using Microsoft.ML.Data;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Models.MlModels;

namespace Mottracker.Application.Services;

public class MlPredictionService : IMlPredictionService
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;
    private readonly PredictionEngine<MotoDemandInput, MotoDemandPrediction> _predictionEngine;

    public MlPredictionService()
    {
        _mlContext = new MLContext(seed: 0);
        
        // Gera dados de treinamento sintéticos baseados em padrões típicos
        var trainingData = GenerateTrainingData();
        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);
        
        // Pipeline de treinamento
        var pipeline = _mlContext.Transforms.Concatenate("Features", 
                nameof(MotoDemandInput.TotalMotos),
                nameof(MotoDemandInput.DiaSemana),
                nameof(MotoDemandInput.Mes),
                nameof(MotoDemandInput.Ano))
            .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "MotosDisponiveis", maximumNumberOfIterations: 100));
        
        // Treina o modelo
        _model = pipeline.Fit(dataView);
        
        // Cria o prediction engine
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<MotoDemandInput, MotoDemandPrediction>(_model);
    }

    public float PredictMotoDemand(int totalMotos, DateTime data)
    {
        var input = new MotoDemandInput
        {
            TotalMotos = totalMotos,
            DiaSemana = (float)data.DayOfWeek,
            Mes = data.Month,
            Ano = data.Year
        };

        var prediction = _predictionEngine.Predict(input);
        return Math.Max(0, Math.Min(totalMotos, prediction.MotosDisponiveisPrevistas)); // Garante valor entre 0 e totalMotos
    }

    // Implement interface: currently no-op. Can be extended to retrain model from persisted data.
    public void TrainFromData(IEnumerable<object> records)
    {
        // No-op for now. Implement retraining logic if needed.
    }

    private List<MotoDemandInput> GenerateTrainingData()
    {
        var random = new Random(42);
        var data = new List<MotoDemandInput>();
        var baseDate = new DateTime(2024, 1, 1);

        // Gera 1000 registros de treinamento
        for (int i = 0; i < 1000; i++)
        {
            var date = baseDate.AddDays(i % 365);
            var totalMotos = random.Next(10, 100);
            
            // Simula padrões: mais disponíveis em dias úteis, menos em fins de semana
            var baseAvailability = totalMotos * 0.7f;
            var dayOfWeekFactor = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday 
                ? 0.6f 
                : 0.8f;
            
            // Fatores sazonais (mais demanda no verão)
            var seasonalFactor = date.Month >= 11 || date.Month <= 2 ? 0.9f : 0.7f;
            
            var disponiveis = baseAvailability * dayOfWeekFactor * seasonalFactor + random.Next(-5, 5);
            
            data.Add(new MotoDemandInput
            {
                TotalMotos = totalMotos,
                DiaSemana = (float)date.DayOfWeek,
                Mes = date.Month,
                Ano = date.Year,
                MotosDisponiveis = Math.Max(0, Math.Min(totalMotos, disponiveis))
            });
        }

        return data;
    }
}

