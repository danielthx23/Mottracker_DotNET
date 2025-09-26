using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IQrCodePontoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>> ObterTodosQrCodePontosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<QrCodePontoResponseDto?>> ObterQrCodePontoPorIdAsync(int id);
        Task<OperationResult<QrCodePontoResponseDto?>> SalvarQrCodePontoAsync(QrCodePontoRequestDto dto);
        Task<OperationResult<QrCodePontoResponseDto?>> EditarQrCodePontoAsync(int id, QrCodePontoRequestDto dto);
        Task<OperationResult<QrCodePontoResponseDto?>> DeletarQrCodePontoAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterTodosQrCodePontosAsync();
        Task<OperationResult<QrCodePontoResponseDto?>> SalvarDadosQrCodePontoAsync(QrCodePontoRequestDto entity);
        Task<OperationResult<QrCodePontoResponseDto?>> EditarDadosQrCodePontoAsync(int id, QrCodePontoRequestDto entity);
        Task<OperationResult<QrCodePontoResponseDto?>> DeletarDadosQrCodePontoAsync(int id);
        Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorIdentificadorAsync(string identificador);
        Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorLayoutPatioIdAsync(int layoutPatioId);
        Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorPosXRangeAsync(float posXMin, float posXMax);
        Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorPosYRangeAsync(float posYMin, float posYMax);
    }
}
