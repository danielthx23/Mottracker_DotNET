using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IQrCodePontoRepository
    {
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<QrCodePontoEntity?> ObterPorIdAsync(int id);
        Task<QrCodePontoEntity?> SalvarAsync(QrCodePontoEntity entity);
        Task<QrCodePontoEntity?> AtualizarAsync(QrCodePontoEntity entity);
        Task<QrCodePontoEntity?> DeletarAsync(int id);
    }
}