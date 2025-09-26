using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface ILayoutPatioUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>> ObterTodosLayoutsPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<LayoutPatioResponseDto?>> ObterLayoutPatioPorIdAsync(int id);
        Task<OperationResult<LayoutPatioResponseDto?>> SalvarLayoutPatioAsync(LayoutPatioRequestDto dto);
        Task<OperationResult<LayoutPatioResponseDto?>> EditarLayoutPatioAsync(int id, LayoutPatioRequestDto dto);
        Task<OperationResult<LayoutPatioResponseDto?>> DeletarLayoutPatioAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterTodosLayoutsPatiosAsync();
        Task<OperationResult<LayoutPatioResponseDto?>> SalvarDadosLayoutPatioAsync(LayoutPatioRequestDto entity);
        Task<OperationResult<LayoutPatioResponseDto?>> EditarDadosLayoutPatioAsync(int id, LayoutPatioRequestDto entity);
        Task<OperationResult<LayoutPatioResponseDto?>> DeletarDadosLayoutPatioAsync(int id);
        Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterLayoutPatioPorPatioIdAsync(int patioId);
        Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterLayoutPatioPorDataCriacaoAsync(DateTime dataCriacao);
    }
}
