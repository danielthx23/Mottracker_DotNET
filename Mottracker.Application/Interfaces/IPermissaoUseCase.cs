using Mottracker.Application.Dtos.Permissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IPermissaoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>> ObterTodosPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<PermissaoResponseDto?>> ObterPermissaoPorIdAsync(int id);
        Task<OperationResult<PermissaoResponseDto?>> SalvarPermissaoAsync(PermissaoRequestDto dto);
        Task<OperationResult<PermissaoResponseDto?>> EditarPermissaoAsync(int id, PermissaoRequestDto dto);
        Task<OperationResult<PermissaoResponseDto?>> DeletarPermissaoAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<PermissaoResponseDto>>> ObterTodosPermissoesAsync();
        Task<OperationResult<PermissaoResponseDto?>> SalvarDadosPermissaoAsync(PermissaoRequestDto entity);
        Task<OperationResult<PermissaoResponseDto?>> EditarDadosPermissaoAsync(int id, PermissaoRequestDto entity);
        Task<OperationResult<PermissaoResponseDto?>> DeletarDadosPermissaoAsync(int id);
        Task<OperationResult<IEnumerable<PermissaoResponseDto>>> ObterPermissaoPorNomeAsync(string nomePermissao);
        Task<OperationResult<IEnumerable<PermissaoResponseDto>>> ObterPermissaoPorDescricaoAsync(string descricao);
    }
}
