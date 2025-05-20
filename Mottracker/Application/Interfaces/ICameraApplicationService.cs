using Mottracker.Application.Dtos.Camera;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Interfaces
{   
    public interface ICameraApplicationService
    {
        IEnumerable<CameraResponseDto> ObterTodasCameras();
         CameraResponseDto? ObterCameraPorId(int id);
         CameraResponseDto? SalvarDadosCamera(CameraRequestDto entity);
         CameraResponseDto? EditarDadosCamera(int id, CameraRequestDto entity);
         CameraResponseDto? DeletarDadosCamera(int id);
        IEnumerable<CameraResponseDto>? ObterCameraPorNome(string nome);
        IEnumerable<CameraResponseDto>? ObterCameraPorStatus(CameraStatus status); 
    }
}
