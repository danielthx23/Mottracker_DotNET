using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface ILayoutPatioRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<LayoutPatioEntity?> ObterPorIdAsync(int id);
        Task<LayoutPatioEntity?> SalvarAsync(LayoutPatioEntity entity);
        Task<LayoutPatioEntity?> AtualizarAsync(LayoutPatioEntity entity);
        Task<LayoutPatioEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterPorPatioIdAsync(int patioId);
        Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterPorDataCriacaoAsync(DateTime dataCriacao);
    }
}
