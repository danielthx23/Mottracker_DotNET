using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Application.Dtos.Moto;

public class MotoResponseDto : MotoDto
{
    public ContratoDto? ContratoMoto { get; set; } = new();
    
    public PatioDto? MotoPatioAtual { get; set; } = new();
}