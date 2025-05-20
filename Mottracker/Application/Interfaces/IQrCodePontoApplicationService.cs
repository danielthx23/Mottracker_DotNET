using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IQrCodePontoApplicationService
    {
        IEnumerable<QrCodePontoResponseDto> ObterTodosQrCodePontos();
        QrCodePontoResponseDto? ObterQrCodePontoPorId(int id);
        QrCodePontoResponseDto? SalvarDadosQrCodePonto(QrCodePontoRequestDto entity);
        QrCodePontoResponseDto? EditarDadosQrCodePonto(int id, QrCodePontoRequestDto entity);
        QrCodePontoResponseDto? DeletarDadosQrCodePonto(int id);
        QrCodePontoResponseDto? ObterQrCodePontoPorIdentificador(string identificadorQrCode);
        IEnumerable<QrCodePontoResponseDto> ObterQrCodePontosPorLayoutPatioId(long layoutPatioId);
        IEnumerable<QrCodePontoResponseDto> ObterQrCodePontosPorPosicaoXEntre(float posXInicial, float posXFinal);
        IEnumerable<QrCodePontoResponseDto> ObterQrCodePontosPorPosicaoYEntre(float posYInicial, float posYFinal);
    }
}
