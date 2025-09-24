using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IPermissaoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>> ObterTodosPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PermissaoResponseDto?>> ObterPermissaoPorIdAsync(int id);
        Task<OperationResult<PermissaoResponseDto?>> SalvarPermissaoAsync(PermissaoRequestDto dto);
        Task<OperationResult<PermissaoResponseDto?>> EditarPermissaoAsync(int id, PermissaoRequestDto dto);
        Task<OperationResult<PermissaoResponseDto?>> DeletarPermissaoAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<PermissaoResponseDto> ObterTodosPermissoes();
        PermissaoResponseDto? ObterPermissaoPorId(int id);
        PermissaoResponseDto? SalvarDadosPermissao(PermissaoRequestDto entity);
        PermissaoResponseDto? EditarDadosPermissao(int id, PermissaoRequestDto entity);
        PermissaoResponseDto? DeletarDadosPermissao(int id);
        IEnumerable<PermissaoResponseDto>? ObterPermissaoPorNome(string nomePermissao);
        IEnumerable<PermissaoResponseDto>? ObterPermissaoPorDescricao(string descricao);
    }
}
