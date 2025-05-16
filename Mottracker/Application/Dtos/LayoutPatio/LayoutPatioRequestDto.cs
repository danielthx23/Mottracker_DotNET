using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.LayoutPatio;

public class LayoutPatioRequestDto : LayoutPatioDto
{
    [Required(ErrorMessage = "O ID do pátio é obrigatório.")]
    public int PatioLayoutPatioId { get; set; }
}