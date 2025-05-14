using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface ILayoutPatioApplicationService
    {
        IEnumerable<LayoutPatioEntity> ObterTodosLayoutsPatios();
        LayoutPatioEntity? ObterLayoutPatioPorId(int id);
        LayoutPatioEntity? SalvarDadosLayoutPatio(LayoutPatioEntity entity);
        LayoutPatioEntity? EditarDadosLayoutPatio(int id, LayoutPatioEntity entity);
        LayoutPatioEntity? DeletarDadosLayoutPatio(int id);
    }
}
