using Mottracker.Application.Dtos.Usuario;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>> ObterTodosUsuariosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorIdAsync(int id);
        Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorEmailAsync(string email);
        Task<OperationResult<UsuarioResponseDto?>> SalvarUsuarioAsync(UsuarioRequestDto dto);
        Task<OperationResult<UsuarioResponseDto?>> EditarUsuarioAsync(int id, UsuarioRequestDto dto);
        Task<OperationResult<UsuarioResponseDto?>> DeletarUsuarioAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<UsuarioResponseDto>>> ObterTodosUsuariosAsync();
        Task<OperationResult<UsuarioResponseDto?>> SalvarDadosUsuarioAsync(UsuarioRequestDto entity);
        Task<OperationResult<UsuarioResponseDto?>> EditarDadosUsuarioAsync(int id, UsuarioRequestDto entity);
        Task<OperationResult<UsuarioResponseDto?>> DeletarDadosUsuarioAsync(int id);
    }
}
