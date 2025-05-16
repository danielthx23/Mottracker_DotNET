using Mottracker.Application.Dtos.Usuario;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IUsuarioApplicationService
    {
        IEnumerable<UsuarioResponseDto> ObterTodosUsuarios();
        UsuarioResponseDto? ObterUsuarioPorId(int id);
        UsuarioResponseDto? SalvarDadosUsuario(UsuarioRequestDto entity);
        UsuarioResponseDto? EditarDadosUsuario(int id, UsuarioRequestDto entity);
        UsuarioResponseDto? DeletarDadosUsuario(int id);
    }
}
