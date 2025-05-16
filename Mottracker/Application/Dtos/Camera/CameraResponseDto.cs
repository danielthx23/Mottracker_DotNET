using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Application.Dtos.Camera;

public class CameraResponseDto : CameraDto
{
    public PatioDto? Patio { get; set; } = new();
}