using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Application.Dtos.Permissao;

public class PermissaoResponseDto : PermissaoDto
{
    public ICollection<UsuarioPermissaoDto>?  UsuarioPermissoes { get; set; } = new List<UsuarioPermissaoDto>();
}