namespace Mottracker.Application.Interfaces;

public interface IMlPredictionService
{
    float PredictMotoDemand(int totalMotos, DateTime data);
    // Optionally method to train/update model from persisted data
    void TrainFromData(IEnumerable<object> records);
}

