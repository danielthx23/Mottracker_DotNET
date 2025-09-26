using Mottracker.Application.Dtos.Contrato;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IContratoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterTodosContratosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<ContratoResponseDto?>> ObterContratoPorIdAsync(int id);
        Task<OperationResult<ContratoResponseDto?>> SalvarContratoAsync(ContratoRequestDto dto);
        Task<OperationResult<ContratoResponseDto?>> EditarContratoAsync(int id, ContratoRequestDto dto);
        Task<OperationResult<ContratoResponseDto?>> DeletarContratoAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterTodosContratosAsync();
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorAtivoContratoAsync(int ativoContrato);
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorUsuarioIdAsync(long usuarioId);
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorMotoIdAsync(long motoId);
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterContratosNaoExpiradosAsync(DateTime dataAtual);
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorRenovacaoAutomaticaAsync(int renovacaoAutomatica);
        Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorDataEntradaEntreAsync(DateTime dataInicio, DateTime dataFim);
    }
}
