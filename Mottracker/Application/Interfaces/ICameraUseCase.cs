using Mottracker.Application.Dtos.Camera;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Interfaces
{
    public interface ICameraUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterTodasCamerasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<CameraResponseDto?>> ObterCameraPorIdAsync(int id);
        Task<OperationResult<CameraResponseDto?>> SalvarCameraAsync(CameraRequestDto dto);
        Task<OperationResult<CameraResponseDto?>> EditarCameraAsync(int id, CameraRequestDto dto);
        Task<OperationResult<CameraResponseDto?>> DeletarCameraAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterCamerasPorNomeAsync(string nome, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterCamerasPorStatusAsync(CameraStatus status, int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}
