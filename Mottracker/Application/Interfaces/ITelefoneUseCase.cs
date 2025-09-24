using Mottracker.Application.Dtos.Telefone;
using Mottracker.Domain.Entities;
namespace Mottracker.Application.Interfaces
{
    public interface ITelefoneUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>> ObterTodosTelefonesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<TelefoneResponseDto?>> ObterTelefonePorIdAsync(int id);
        Task<OperationResult<TelefoneResponseDto?>> SalvarTelefoneAsync(TelefoneRequestDto dto);
        Task<OperationResult<TelefoneResponseDto?>> EditarTelefoneAsync(int id, TelefoneRequestDto dto);
        Task<OperationResult<TelefoneResponseDto?>> DeletarTelefoneAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<TelefoneResponseDto> ObterTodosTelefones();
        TelefoneResponseDto? ObterTelefonePorId(int id);
        TelefoneResponseDto? SalvarDadosTelefone(TelefoneRequestDto entity);
        TelefoneResponseDto? EditarDadosTelefone(int id, TelefoneRequestDto entity);
        TelefoneResponseDto? DeletarDadosTelefone(int id);
        IEnumerable<TelefoneResponseDto>? ObterTelefonePorNumero(string numero);
        IEnumerable<TelefoneResponseDto>? ObterTelefonePorUsuarioId(int usuarioId);
        IEnumerable<TelefoneResponseDto>? ObterTelefonePorTipo(string tipo);
    }
}
