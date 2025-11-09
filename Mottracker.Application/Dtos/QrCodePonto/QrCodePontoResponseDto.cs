using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Application.Dtos.QrCodePonto;

public class QrCodePontoResponseDto : QrCodePontoDto
{
    public LayoutPatioDto? LayoutPatio { get; set; }
}