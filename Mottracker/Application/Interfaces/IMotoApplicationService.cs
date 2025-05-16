using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IMotoApplicationService
    {
        IEnumerable<MotoResponseDto> ObterTodasMotos();
        MotoResponseDto? ObterMotoPorId(int id);
        MotoResponseDto? SalvarDadosMoto(MotoRequestDto entity);
        MotoResponseDto? EditarDadosMoto(int id, MotoRequestDto entity);
        MotoResponseDto? DeletarDadosMoto(int id);
    }
}
