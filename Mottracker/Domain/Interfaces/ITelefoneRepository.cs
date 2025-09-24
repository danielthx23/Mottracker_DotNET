using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface ITelefoneRepository
    {
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<TelefoneEntity?> ObterPorIdAsync(int id);
        Task<TelefoneEntity?> SalvarAsync(TelefoneEntity entity);
        Task<TelefoneEntity?> AtualizarAsync(TelefoneEntity entity);
        Task<TelefoneEntity?> DeletarAsync(int id);
    }
}