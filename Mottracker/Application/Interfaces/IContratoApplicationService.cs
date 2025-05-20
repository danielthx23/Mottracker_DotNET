using Mottracker.Application.Dtos.Contrato;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IContratoApplicationService
    {
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
