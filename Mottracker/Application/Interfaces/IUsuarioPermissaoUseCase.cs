using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IUsuarioPermissaoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>> ObterTodosUsuarioPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<UsuarioPermissaoResponseDto?>> ObterUsuarioPermissaoPorIdAsync(int usuarioId, int permissaoId);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> SalvarUsuarioPermissaoAsync(UsuarioPermissaoRequestDto dto);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> EditarUsuarioPermissaoAsync(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto dto);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> DeletarUsuarioPermissaoAsync(int usuarioId, int permissaoId);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterTodosUsuarioPermissoesAsync();
        Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterUsuarioPermissoesPorUsuarioIdAsync(long usuarioId);
        Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterUsuarioPermissoesPorPermissaoIdAsync(long permissaoId);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> SalvarDadosUsuarioPermissaoAsync(UsuarioPermissaoRequestDto entity);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> EditarDadosUsuarioPermissaoAsync(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto entity);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> DeletarDadosUsuarioPermissaoAsync(int usuarioId, int permissaoId);
    }
}
