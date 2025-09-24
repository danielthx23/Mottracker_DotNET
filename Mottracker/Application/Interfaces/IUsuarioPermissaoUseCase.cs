using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IUsuarioPermissaoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>> ObterTodosUsuarioPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> ObterUsuarioPermissaoPorIdAsync(int usuarioId, int permissaoId);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> SalvarUsuarioPermissaoAsync(UsuarioPermissaoRequestDto dto);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> EditarUsuarioPermissaoAsync(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto dto);
        Task<OperationResult<UsuarioPermissaoResponseDto?>> DeletarUsuarioPermissaoAsync(int usuarioId, int permissaoId);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<UsuarioPermissaoResponseDto> ObterTodosUsuarioPermissoes();
        UsuarioPermissaoResponseDto? ObterUsuarioPermissaoPorId(int usuarioId, int permissaoId);
        IEnumerable<UsuarioPermissaoResponseDto>? ObterUsuarioPermissoesPorUsuarioId(long usuarioId);
        IEnumerable<UsuarioPermissaoResponseDto>? ObterUsuarioPermissoesPorPermissaoId(long permissaoId);
        UsuarioPermissaoResponseDto? SalvarDadosUsuarioPermissao(UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? EditarDadosUsuarioPermissao(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto entity);
        UsuarioPermissaoResponseDto? DeletarDadosUsuarioPermissao(int usuarioId, int permissaoId);
    }
}
