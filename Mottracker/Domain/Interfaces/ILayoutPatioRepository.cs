using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface ILayoutPatioRepository
    {
        Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<LayoutPatioEntity?> ObterPorIdAsync(int id);
        Task<LayoutPatioEntity?> SalvarAsync(LayoutPatioEntity entity);
        Task<LayoutPatioEntity?> AtualizarAsync(LayoutPatioEntity entity);
        Task<LayoutPatioEntity?> DeletarAsync(int id);
    }
}
