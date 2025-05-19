using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.LayoutPatio;

public class LayoutPatioRequestDto : LayoutPatioDto
{
    public int? PatioLayoutPatioId { get; set; }
}