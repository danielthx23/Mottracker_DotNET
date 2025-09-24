using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IContratoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterTodosContratosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<ContratoResponseDto?>> ObterContratoPorIdAsync(int id);
        Task<OperationResult<ContratoResponseDto?>> SalvarContratoAsync(ContratoRequestDto dto);
        Task<OperationResult<ContratoResponseDto?>> EditarContratoAsync(int id, ContratoRequestDto dto);
        Task<OperationResult<ContratoResponseDto?>> DeletarContratoAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<ContratoResponseDto> ObterTodosContratos();
        ContratoResponseDto? ObterContratoPorId(int id);
        ContratoResponseDto? SalvarDadosContrato(ContratoRequestDto entity);
        ContratoResponseDto? EditarDadosContrato(int id, ContratoRequestDto entity);
        ContratoResponseDto? DeletarDadosContrato(int id);
        IEnumerable<ContratoResponseDto> ObterPorAtivoContrato(int ativoContrato);
        IEnumerable<ContratoResponseDto> ObterPorUsuarioId(long usuarioId);
        IEnumerable<ContratoResponseDto> ObterPorMotoId(long motoId);
        IEnumerable<ContratoResponseDto> ObterContratosNaoExpirados(DateTime dataAtual);
        IEnumerable<ContratoResponseDto> ObterPorRenovacaoAutomatica(int renovacaoAutomatica);
        IEnumerable<ContratoResponseDto> ObterPorDataEntradaEntre(DateTime dataInicio, DateTime dataFim);
    }
}
