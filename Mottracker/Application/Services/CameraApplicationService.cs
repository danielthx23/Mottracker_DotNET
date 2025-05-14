using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class CameraApplicationService : ICameraApplicationService
    {
        private readonly ICameraRepository _repository;
      
        public CameraApplicationService(ICameraRepository repository)
        {
            _repository = repository;
        }
      
        public CameraEntity? ObterCameraPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<CameraEntity> ObterTodasCameras() 
        {
            return _repository.ObterTodos();
        }
              
        public CameraEntity? SalvarDadosCamera(CameraEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public CameraEntity? EditarDadosCamera(int id, CameraEntity entity)
        {
            var cameraExistente = _repository.ObterPorId(id);
      
            if (cameraExistente == null)
                return null;
                  
            cameraExistente.NomeCamera = entity.NomeCamera;
            cameraExistente.IpCamera = entity.IpCamera;
            cameraExistente.Status = entity.Status;
            cameraExistente.PosX = entity.PosX;
            cameraExistente.PosY = entity.PosY;
            cameraExistente.Patio = entity.Patio;
      
            return _repository.Atualizar(cameraExistente);
        }
              
        public CameraEntity? DeletarDadosCamera(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
