using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Interfaces
{
    public interface ICameraRepository
    {
        Task<PageResultModel<IEnumerable<CameraEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<CameraEntity?> ObterPorIdAsync(int id);
        Task<CameraEntity?> SalvarAsync(CameraEntity entity);
        Task<CameraEntity?> AtualizarAsync(CameraEntity entity);
        Task<CameraEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<CameraEntity>>> ObterPorNomeAsync(string nome, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PageResultModel<IEnumerable<CameraEntity>>> ObterPorStatusAsync(CameraStatus status, int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}