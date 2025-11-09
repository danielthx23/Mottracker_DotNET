using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Dtos.Patio;

public class PatioResponseDto : PatioDto
{
    public ICollection<MotoDto>? MotosPatioAtual { get; set; } = new List<MotoDto>();

    public ICollection<CameraDto>? CamerasPatio { get; set; } = new List<CameraDto>();
    
    public LayoutPatioDto? LayoutPatio { get; set; }
    
    public EnderecoDto? EnderecoPatio { get; set; }
}