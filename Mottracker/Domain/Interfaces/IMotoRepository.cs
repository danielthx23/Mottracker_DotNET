using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IMotoRepository
    {
        IEnumerable<MotoEntity> ObterTodos();
        MotoEntity? ObterPorId(int id);
        MotoEntity? Salvar(MotoEntity entity);
        MotoEntity? Atualizar(MotoEntity entity);
        MotoEntity? Deletar(int id);
    }
}
