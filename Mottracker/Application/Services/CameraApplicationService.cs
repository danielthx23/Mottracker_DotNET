using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Application.Services
{
    public class CameraApplicationService : ICameraApplicationService
    {
        private readonly ICameraRepository _repository;

        public CameraApplicationService(ICameraRepository repository)
        {
            _repository = repository;
        }

        public CameraResponseDto? ObterCameraPorId(int id)
        {
            var camera = _repository.ObterPorId(id); 

            if (camera == null) return null;

            return MapToResponseDto(camera);
        }

        public IEnumerable<CameraResponseDto> ObterTodasCameras()
        {
            var cameras = _repository.ObterTodos();
            return cameras.Select(MapToResponseDto);
        }

        public CameraResponseDto? SalvarDadosCamera(CameraRequestDto entity)
        {
            var cameraEntity = new CameraEntity
            {
                NomeCamera = entity.NomeCamera,
                IpCamera = entity.IpCamera,
                Status = entity.Status,
                PosX = entity.PosX,
                PosY = entity.PosY,
                PatioId = entity.PatioId
            };

            var saved = _repository.Salvar(cameraEntity);
            if (saved == null) return null;
            
            return MapToResponseDto(saved);
        }

        public CameraResponseDto? EditarDadosCamera(int id, CameraRequestDto entity)
        {
            var cameraExistente = _repository.ObterPorId(id);
            if (cameraExistente == null) return null;

            cameraExistente.NomeCamera = entity.NomeCamera;
            cameraExistente.IpCamera = entity.IpCamera;
            cameraExistente.Status = entity.Status;
            cameraExistente.PosX = entity.PosX;
            cameraExistente.PosY = entity.PosY;
            cameraExistente.PatioId = entity.PatioId;

            var updated = _repository.Atualizar(cameraExistente);
            if (updated == null) return null;

            return MapToResponseDto(updated);
        }

        public CameraResponseDto? DeletarDadosCamera(int id)
        {
            var deleted = _repository.Deletar(id);
            if (deleted == null) return null;
            
            return MapToResponseDto(deleted);
        }

        private CameraResponseDto MapToResponseDto(CameraEntity camera)
        {
            return new CameraResponseDto
            {
                IdCamera = camera.IdCamera,
                NomeCamera = camera.NomeCamera,
                IpCamera = camera.IpCamera,
                Status = camera.Status,
                PosX = camera.PosX,
                PosY = camera.PosY,
                Patio = camera.Patio != null ? new PatioDto
                {
                    IdPatio = camera.Patio.IdPatio,
                    NomePatio = camera.Patio.NomePatio,
                    MotosTotaisPatio = camera.Patio.MotosTotaisPatio,
                    MotosDisponiveisPatio = camera.Patio.MotosDisponiveisPatio,
                    DataPatio = camera.Patio.DataPatio
                } : null
            };
        }
    }
}
