using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IQrCodePontoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<QrCodePontoEntity?> ObterPorIdAsync(int id);
        Task<QrCodePontoEntity?> SalvarAsync(QrCodePontoEntity entity);
        Task<QrCodePontoEntity?> AtualizarAsync(QrCodePontoEntity entity);
        Task<QrCodePontoEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorIdentificadorAsync(string identificador);
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorLayoutPatioIdAsync(int layoutPatioId);
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorPosXRangeAsync(float posXMin, float posXMax);
        Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorPosYRangeAsync(float posYMin, float posYMax);
    }
}