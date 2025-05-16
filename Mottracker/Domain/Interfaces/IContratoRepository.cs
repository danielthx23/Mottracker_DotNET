using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IContratoRepository
    {
        IEnumerable<ContratoEntity> ObterTodos();
        ContratoEntity? ObterPorId(int id);
        List<ContratoEntity>? ObterPorIds(List<int> id);
        ContratoEntity? Salvar(ContratoEntity entity);
        ContratoEntity? Atualizar(ContratoEntity entity);
        ContratoEntity? Deletar(int id);
    }
}
