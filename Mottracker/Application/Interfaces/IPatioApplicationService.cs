using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IPatioApplicationService
    {
        IEnumerable<PatioEntity> ObterTodosPatios();
        PatioEntity? ObterPatioPorId(int id);
        PatioEntity? SalvarDadosPatio(PatioEntity entity);
        PatioEntity? EditarDadosPatio(int id, PatioEntity entity);
        PatioEntity? DeletarDadosPatio(int id);
    }
}
