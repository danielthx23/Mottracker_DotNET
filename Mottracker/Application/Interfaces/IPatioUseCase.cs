using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IPatioUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>> ObterTodosPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PatioResponseDto?>> ObterPatioPorIdAsync(int id);
        Task<OperationResult<PatioResponseDto?>> SalvarPatioAsync(PatioRequestDto dto);
        Task<OperationResult<PatioResponseDto?>> EditarPatioAsync(int id, PatioRequestDto dto);
        Task<OperationResult<PatioResponseDto?>> DeletarPatioAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<PatioResponseDto> ObterTodosPatios();
        PatioResponseDto? ObterPatioPorId(int id);
        PatioResponseDto? SalvarDadosPatio(PatioRequestDto entity);
        PatioResponseDto? EditarDadosPatio(int id, PatioRequestDto entity);
        PatioResponseDto? DeletarDadosPatio(int id);
        IEnumerable<PatioResponseDto>? ObterPatioPorNome(string nomePatio);
        IEnumerable<PatioResponseDto>? ObterPatioPorMotosDisponiveis(int motosDisponiveis);
        IEnumerable<PatioResponseDto>? ObterPatioPorDataPosterior(DateTime data);
        IEnumerable<PatioResponseDto>? ObterPatioPorDataAnterior(DateTime data);
    }
}
