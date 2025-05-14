using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface ITelefoneApplicationService
    {
        IEnumerable<TelefoneEntity> ObterTodosTelefones();
        TelefoneEntity? ObterTelefonePorId(int id);
        TelefoneEntity? SalvarDadosTelefone(TelefoneEntity entity);
        TelefoneEntity? EditarDadosTelefone(int id, TelefoneEntity entity);
        TelefoneEntity? DeletarDadosTelefone(int id);
    }
}
