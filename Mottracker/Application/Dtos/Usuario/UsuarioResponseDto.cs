using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Application.Dtos.Usuario;

public class UsuarioResponseDto : UsuarioDto
{
    public ContratoDto? ContratoUsuario { get; set; }

    public ICollection<TelefoneDto>? Telefones { get; set; } = new List<TelefoneDto>();

    public ICollection<UsuarioPermissaoDto>? UsuarioPermissoes { get; set; } = new List<UsuarioPermissaoDto>();
}