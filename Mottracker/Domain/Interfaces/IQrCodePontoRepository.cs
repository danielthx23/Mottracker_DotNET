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
    }
}
