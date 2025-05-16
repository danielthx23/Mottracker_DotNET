using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface ILayoutPatioApplicationService
    {
        IEnumerable<LayoutPatioResponseDto> ObterTodosLayoutsPatios();
        LayoutPatioResponseDto? ObterLayoutPatioPorId(int id);
        LayoutPatioResponseDto? SalvarDadosLayoutPatio(LayoutPatioRequestDto entity);
        LayoutPatioResponseDto? EditarDadosLayoutPatio(int id, LayoutPatioRequestDto entity);
        LayoutPatioResponseDto? DeletarDadosLayoutPatio(int id);
    }
}
