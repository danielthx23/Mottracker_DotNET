using Mottracker.Application.Dtos.Patio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IPatioUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>> ObterTodosPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<PatioResponseDto?>> ObterPatioPorIdAsync(int id);
        Task<OperationResult<PatioResponseDto?>> SalvarPatioAsync(PatioRequestDto dto);
        Task<OperationResult<PatioResponseDto?>> EditarPatioAsync(int id, PatioRequestDto dto);
        Task<OperationResult<PatioResponseDto?>> DeletarPatioAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<PatioResponseDto>>> ObterTodosPatiosAsync();
        Task<OperationResult<PatioResponseDto?>> SalvarDadosPatioAsync(PatioRequestDto entity);
        Task<OperationResult<PatioResponseDto?>> EditarDadosPatioAsync(int id, PatioRequestDto entity);
        Task<OperationResult<PatioResponseDto?>> DeletarDadosPatioAsync(int id);
        Task<OperationResult<IEnumerable<PatioResponseDto>>> ObterPatioPorNomeAsync(string nomePatio);
        Task<OperationResult<IEnumerable<PatioResponseDto>>> ObterPatioPorMotosDisponiveisAsync(int motosDisponiveis);
        Task<OperationResult<IEnumerable<PatioResponseDto>>> ObterPatioPorDataPosteriorAsync(DateTime data);
        Task<OperationResult<IEnumerable<PatioResponseDto>>> ObterPatioPorDataAnteriorAsync(DateTime data);
    }
}
