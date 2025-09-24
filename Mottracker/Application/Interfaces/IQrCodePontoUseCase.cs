using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IQrCodePontoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>> ObterTodosQrCodePontosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<QrCodePontoResponseDto?>> ObterQrCodePontoPorIdAsync(int id);
        Task<OperationResult<QrCodePontoResponseDto?>> SalvarQrCodePontoAsync(QrCodePontoRequestDto dto);
        Task<OperationResult<QrCodePontoResponseDto?>> EditarQrCodePontoAsync(int id, QrCodePontoRequestDto dto);
        Task<OperationResult<QrCodePontoResponseDto?>> DeletarQrCodePontoAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<QrCodePontoResponseDto> ObterTodosQrCodePontos();
        QrCodePontoResponseDto? ObterQrCodePontoPorId(int id);
        QrCodePontoResponseDto? SalvarDadosQrCodePonto(QrCodePontoRequestDto entity);
        QrCodePontoResponseDto? EditarDadosQrCodePonto(int id, QrCodePontoRequestDto entity);
        QrCodePontoResponseDto? DeletarDadosQrCodePonto(int id);
        IEnumerable<QrCodePontoResponseDto>? ObterQrCodePontoPorIdentificador(string identificador);
        IEnumerable<QrCodePontoResponseDto>? ObterQrCodePontoPorLayoutPatioId(int layoutPatioId);
        IEnumerable<QrCodePontoResponseDto>? ObterQrCodePontoPorPosXRange(float posXMin, float posXMax);
        IEnumerable<QrCodePontoResponseDto>? ObterQrCodePontoPorPosYRange(float posYMin, float posYMax);
    }
}
