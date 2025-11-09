using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Application.Dtos.Telefone;

public class TelefoneResponseDto : TelefoneDto
{
    public UsuarioDto? Usuario { get; set; }
}