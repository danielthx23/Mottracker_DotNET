using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface ILayoutPatioUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>> ObterTodosLayoutsPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<LayoutPatioResponseDto?>> ObterLayoutPatioPorIdAsync(int id);
        Task<OperationResult<LayoutPatioResponseDto?>> SalvarLayoutPatioAsync(LayoutPatioRequestDto dto);
        Task<OperationResult<LayoutPatioResponseDto?>> EditarLayoutPatioAsync(int id, LayoutPatioRequestDto dto);
        Task<OperationResult<LayoutPatioResponseDto?>> DeletarLayoutPatioAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<LayoutPatioResponseDto> ObterTodosLayoutsPatios();
        LayoutPatioResponseDto? ObterLayoutPatioPorId(int id);
        LayoutPatioResponseDto? SalvarDadosLayoutPatio(LayoutPatioRequestDto entity);
        LayoutPatioResponseDto? EditarDadosLayoutPatio(int id, LayoutPatioRequestDto entity);
        LayoutPatioResponseDto? DeletarDadosLayoutPatio(int id);
        IEnumerable<LayoutPatioResponseDto>? ObterLayoutPatioPorPatioId(int patioId);
        IEnumerable<LayoutPatioResponseDto>? ObterLayoutPatioPorDataCriacao(DateTime dataCriacao);
    }
}
