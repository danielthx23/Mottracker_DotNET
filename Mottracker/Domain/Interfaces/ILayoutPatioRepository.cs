using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface ILayoutPatioRepository
    {
        IEnumerable<LayoutPatioEntity> ObterTodos();
        LayoutPatioEntity? ObterPorId(int id);
        LayoutPatioEntity? Salvar(LayoutPatioEntity entity);
        LayoutPatioEntity? Atualizar(LayoutPatioEntity entity);
        LayoutPatioEntity? Deletar(int id);
        IEnumerable<LayoutPatioEntity> ObterPorIdPatio(long patioId);
        IEnumerable<LayoutPatioEntity> ObterPorDataCriacaoEntre(DateTime dataInicio, DateTime dataFim);
    }
}
