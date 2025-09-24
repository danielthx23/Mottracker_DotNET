using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<MotoEntity?> ObterPorIdAsync(int id);
        Task<MotoEntity?> SalvarAsync(MotoEntity entity);
        Task<MotoEntity?> AtualizarAsync(MotoEntity entity);
        Task<MotoEntity?> DeletarAsync(int id);
        Task<MotoEntity?> ObterPorPlacaAsync(string placa);
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}