using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Models;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Interfaces
{
    public interface IMotoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterTodasMotosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<MotoResponseDto?>> ObterMotoPorIdAsync(int id);
        Task<OperationResult<MotoResponseDto?>> ObterMotoPorPlacaAsync(string placa);
        Task<OperationResult<MotoResponseDto?>> SalvarMotoAsync(MotoRequestDto dto);
        Task<OperationResult<MotoResponseDto?>> EditarMotoAsync(int id, MotoRequestDto dto);
        Task<OperationResult<MotoResponseDto?>> DeletarMotoAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterMotosPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<MotoResponseDto> ObterTodasMotos();
        MotoResponseDto? ObterMotoPorId(int id);
        MotoResponseDto? ObterMotoPorPlaca(string placaMoto);
        IEnumerable<MotoResponseDto>? ObterMotoPorEstado(Estados estadoMoto);
        IEnumerable<MotoResponseDto>? ObterMotoPorContratoId(long contratoId);
        MotoResponseDto? SalvarDadosMoto(MotoRequestDto entity);
        MotoResponseDto? EditarDadosMoto(int id, MotoRequestDto entity);
        MotoResponseDto? DeletarDadosMoto(int id);
    }
}
