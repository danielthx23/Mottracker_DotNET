using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Application.Dtos.Contrato;

public class ContratoResponseDto : ContratoDto
{
    public UsuarioDto? UsuarioContrato { get; set; } = new();
    
    public MotoDto? MotoContrato { get; set; } = new();
}