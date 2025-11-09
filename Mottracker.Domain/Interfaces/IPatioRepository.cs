using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IPatioRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<PatioEntity?> ObterPorIdAsync(int id);
        Task<PatioEntity?> SalvarAsync(PatioEntity entity);
        Task<PatioEntity?> AtualizarAsync(PatioEntity entity);
        Task<PatioEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterPorNomeAsync(string nomePatio);
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterPorMotosDisponiveisAsync(int motosDisponiveis);
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterPorDataPosteriorAsync(DateTime data);
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterPorDataAnteriorAsync(DateTime data);
    }
}
