using Mottracker.Application.Dtos.Telefone;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface ITelefoneUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>> ObterTodosTelefonesAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<TelefoneResponseDto?>> ObterTelefonePorIdAsync(int id);
        Task<OperationResult<TelefoneResponseDto?>> SalvarTelefoneAsync(TelefoneRequestDto dto);
        Task<OperationResult<TelefoneResponseDto?>> EditarTelefoneAsync(int id, TelefoneRequestDto dto);
        Task<OperationResult<TelefoneResponseDto?>> DeletarTelefoneAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTodosTelefonesAsync();
        Task<OperationResult<TelefoneResponseDto?>> SalvarDadosTelefoneAsync(TelefoneRequestDto entity);
        Task<OperationResult<TelefoneResponseDto?>> EditarDadosTelefoneAsync(int id, TelefoneRequestDto entity);
        Task<OperationResult<TelefoneResponseDto?>> DeletarDadosTelefoneAsync(int id);
        Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorNumeroAsync(string numero);
        Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorUsuarioIdAsync(int usuarioId);
        Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorTipoAsync(string tipo);
    }
}
