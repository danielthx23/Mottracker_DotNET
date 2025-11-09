using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Camera;
using Mottracker.Domain.Enums;

namespace Mottracker.Docs.Samples
{
    public class CameraRequestDtoSample : IExamplesProvider<CameraRequestDto>
    {
        public CameraRequestDto GetExamples()
        {
            return new CameraRequestDto
            {
                IdCamera = 0,
                NomeCamera = "Câmera Entrada Lateral",
                IpCamera = "192.168.1.20",
                Status = CameraStatus.Ativa,
                PosX = 200.00f,
                PosY = 150.00f,
                PatioId = 2
            };
        }
    }
}
