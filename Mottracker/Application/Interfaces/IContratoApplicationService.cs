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
    }
}
