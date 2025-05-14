using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface ICameraApplicationService
    {
        IEnumerable<CameraEntity> ObterTodasCameras();
        CameraEntity? ObterCameraPorId(int id);
        CameraEntity? SalvarDadosCamera(CameraEntity entity);
        CameraEntity? EditarDadosCamera(int id, CameraEntity entity);
        CameraEntity? DeletarDadosCamera(int id);
    }
}
