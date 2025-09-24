using Mottracker.Application.Dtos.Usuario;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>> ObterTodosUsuariosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorIdAsync(int id);
        Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorEmailAsync(string email);
        Task<OperationResult<UsuarioResponseDto?>> SalvarUsuarioAsync(UsuarioRequestDto dto);
        Task<OperationResult<UsuarioResponseDto?>> EditarUsuarioAsync(int id, UsuarioRequestDto dto);
        Task<OperationResult<UsuarioResponseDto?>> DeletarUsuarioAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<UsuarioResponseDto> ObterTodosUsuarios();
        UsuarioResponseDto? ObterUsuarioPorId(int id);
        UsuarioResponseDto? ObterUsuarioPorEmail(string emailUsuario);
        UsuarioResponseDto? SalvarDadosUsuario(UsuarioRequestDto entity);
        UsuarioResponseDto? EditarDadosUsuario(int id, UsuarioRequestDto entity);
        UsuarioResponseDto? DeletarDadosUsuario(int id);
    }
}
