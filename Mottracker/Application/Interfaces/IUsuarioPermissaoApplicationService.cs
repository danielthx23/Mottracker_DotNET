using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IUsuarioPermissaoApplicationService
    {
        IEnumerable<UsuarioPermissaoResponseDto> ObterTodosUsuarioPermissoes();
        UsuarioPermissaoResponseDto? ObterUsuarioPermissaoPorId(int usuarioId, int permissaoId);
        UsuarioPermissaoResponseDto? SalvarDadosUsuarioPermissao(UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? EditarDadosUsuarioPermissao(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? DeletarDadosUsuarioPermissao(int usuarioId, int permissaoId);
    }
}
