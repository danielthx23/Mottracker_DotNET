using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Interfaces
{
    public interface IMotoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterTodasMotosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterMotosPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<MotoResponseDto?>> ObterMotoPorIdAsync(int id);
        Task<OperationResult<MotoResponseDto?>> ObterMotoPorPlacaAsync(string placa);
        Task<OperationResult<MotoResponseDto?>> SalvarMotoAsync(MotoRequestDto dto);
        Task<OperationResult<MotoResponseDto?>> EditarMotoAsync(int id, MotoRequestDto dto);
        Task<OperationResult<MotoResponseDto?>> DeletarMotoAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<MotoResponseDto>>> ObterTodasMotosAsync();
        Task<OperationResult<IEnumerable<MotoResponseDto>>> ObterMotoPorEstadoAsync(Estados estadoMoto);
        Task<OperationResult<IEnumerable<MotoResponseDto>>> ObterMotoPorContratoIdAsync(long contratoId);
        Task<OperationResult<MotoResponseDto?>> SalvarDadosMotoAsync(MotoRequestDto entity);
        Task<OperationResult<MotoResponseDto?>> EditarDadosMotoAsync(int id, MotoRequestDto entity);
        Task<OperationResult<MotoResponseDto?>> DeletarDadosMotoAsync(int id);
    }
}
