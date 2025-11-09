using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Application.Dtos.UsuarioPermissao;

public class UsuarioPermissaoResponseDto : UsuarioPermissaoDto
{
    public UsuarioDto? Usuario { get; set; }

    public PermissaoDto? Permissao { get; set; }
}