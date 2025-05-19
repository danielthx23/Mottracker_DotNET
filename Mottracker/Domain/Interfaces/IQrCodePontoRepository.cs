using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IQrCodePontoRepository
    {
        IEnumerable<QrCodePontoEntity> ObterTodos();
        QrCodePontoEntity? ObterPorId(int id);
        QrCodePontoEntity? Salvar(QrCodePontoEntity entity);
        QrCodePontoEntity? Atualizar(QrCodePontoEntity entity);
        QrCodePontoEntity? Deletar(int id);
        QrCodePontoEntity? ObterPorIdentificador(string identificadorQrCode);
        IEnumerable<QrCodePontoEntity> ObterPorIdLayoutPatio(long layoutPatioId);
        IEnumerable<QrCodePontoEntity> ObterPorPosicaoXEntre(float posXInicial, float posXFinal);
        IEnumerable<QrCodePontoEntity> ObterPorPosicaoYEntre(float posYInicial, float posYFinal);
    }
}