using Mottracker.Application.Dtos.Camera;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class CameraMapper
    {
        public static CameraEntity ToCameraEntity(this CameraRequestDto obj)
        {
            return new CameraEntity
            {
                IdCamera = obj.IdCamera,
                NomeCamera = obj.NomeCamera,
                IpCamera = obj.IpCamera,
                Status = obj.Status,
                PosX = obj.PosX,
                PosY = obj.PosY,
                PatioId = obj.PatioId
            };
        }

        public static CameraDto ToCameraDto(this CameraEntity obj)
        {
            return new CameraDto
            {
                IdCamera = obj.IdCamera,
                NomeCamera = obj.NomeCamera,
                IpCamera = obj.IpCamera,
                Status = obj.Status,
                PosX = obj.PosX,
                PosY = obj.PosY
            };
        }

        public static CameraResponseDto ToCameraResponseDto(this CameraEntity obj)
        {
            return new CameraResponseDto
            {
                IdCamera = obj.IdCamera,
                NomeCamera = obj.NomeCamera,
                IpCamera = obj.IpCamera,
                Status = obj.Status,
                PosX = obj.PosX,
                PosY = obj.PosY,
                Patio = obj.Patio?.ToPatioDto()
            };
        }
    }
}
