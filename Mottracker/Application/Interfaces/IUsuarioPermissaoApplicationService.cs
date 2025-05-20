using Mottracker.Application.Dtos.UsuarioPermissao;
using System.Collections.Generic;

namespace Mottracker.Application.Interfaces
{
    public interface IUsuarioPermissaoApplicationService
    {
        IEnumerable<UsuarioPermissaoResponseDto> ObterTodosUsuarioPermissoes();
        UsuarioPermissaoResponseDto? ObterUsuarioPermissaoPorId(int usuarioId, int permissaoId);
        IEnumerable<UsuarioPermissaoResponseDto> ObterUsuarioPermissoesPorUsuarioId(long usuarioId);  // NOVO
        IEnumerable<UsuarioPermissaoResponseDto> ObterUsuarioPermissoesPorPermissaoId(long permissaoId); // NOVO
        UsuarioPermissaoResponseDto? SalvarDadosUsuarioPermissao(UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? EditarDadosUsuarioPermissao(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? DeletarDadosUsuarioPermissao(int usuarioId, int permissaoId);
    }
}
