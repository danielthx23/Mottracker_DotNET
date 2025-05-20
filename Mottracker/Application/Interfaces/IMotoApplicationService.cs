using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Interfaces
{   
    public interface IMotoApplicationService
    {
        IEnumerable<MotoResponseDto> ObterTodasMotos();
        MotoResponseDto? ObterMotoPorId(int id);
        MotoResponseDto? ObterMotoPorPlaca(string placaMoto);
        IEnumerable<MotoResponseDto> ObterMotosPorEstado(Estados estadoMoto);
        IEnumerable<MotoResponseDto> ObterMotosPorContratoId(long contratoId);
        MotoResponseDto? SalvarDadosMoto(MotoRequestDto entity);
        MotoResponseDto? EditarDadosMoto(int id, MotoRequestDto entity);
        MotoResponseDto? DeletarDadosMoto(int id);
    }
}
