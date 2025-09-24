using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PatioEntity?> ObterPorIdAsync(int id);
        Task<PatioEntity?> SalvarAsync(PatioEntity entity);
        Task<PatioEntity?> AtualizarAsync(PatioEntity entity);
        Task<PatioEntity?> DeletarAsync(int id);
    }
}
