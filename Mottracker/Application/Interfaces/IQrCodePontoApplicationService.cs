using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IQrCodePontoApplicationService
    {
        IEnumerable<QrCodePontoEntity> ObterTodosQrCodePontos();
        QrCodePontoEntity? ObterQrCodePontoPorId(int id);
        QrCodePontoEntity? SalvarDadosQrCodePonto(QrCodePontoEntity entity);
        QrCodePontoEntity? EditarDadosQrCodePonto(int id, QrCodePontoEntity entity);
        QrCodePontoEntity? DeletarDadosQrCodePonto(int id);
    }
}
