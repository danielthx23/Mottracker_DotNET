using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Interfaces
{   
    public interface IMotoRepository
    {
        IEnumerable<MotoEntity> ObterTodos();
        MotoEntity? ObterPorId(int id);
        MotoEntity? Salvar(MotoEntity entity);
        MotoEntity? Atualizar(MotoEntity entity);
        MotoEntity? Deletar(int id);
        MotoEntity? ObterPorPlaca(string placaMoto);
        IEnumerable<MotoEntity> ObterPorEstado(Estados estadoMoto);
        IEnumerable<MotoEntity> ObterPorIdContrato(long contratoId);
    }
}