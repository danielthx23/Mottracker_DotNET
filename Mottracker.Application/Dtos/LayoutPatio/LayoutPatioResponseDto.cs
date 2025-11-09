using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Dtos.QrCodePonto;

namespace Mottracker.Application.Dtos.LayoutPatio;

public class LayoutPatioResponseDto : LayoutPatioDto
{
    public PatioDto? PatioLayoutPatio { get; set; } = new();
    
    public virtual ICollection<QrCodePontoDto>? QrCodesLayoutPatio { get; set; } = new List<QrCodePontoDto>();
}