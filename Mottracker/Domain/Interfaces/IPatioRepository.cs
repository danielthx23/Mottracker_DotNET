using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IPatioRepository
    {
        IEnumerable<PatioEntity> ObterTodos();
        PatioEntity? ObterPorId(int id);
        List<PatioEntity>? ObterPorIds(List<int> id);
        PatioEntity? Salvar(PatioEntity entity);
        PatioEntity? Atualizar(PatioEntity entity);
        PatioEntity? Deletar(int id);
    }
}
