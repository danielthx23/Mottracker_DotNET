using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Domain.Enums;

namespace Mottracker.Docs.Samples
{
    public class CameraResponseListSample : IExamplesProvider<IEnumerable<CameraResponseDto>>
    {
        public IEnumerable<CameraResponseDto> GetExamples()
        {
            return new List<CameraResponseDto>
            {
                new CameraResponseDto
                {
                    IdCamera = 1,
                    NomeCamera = "Câmera Portão Principal",
                    IpCamera = "192.168.1.10",
                    Status = CameraStatus.Ativa,
                    PosX = 123.45f,
                    PosY = 67.89f,
                    Patio = new PatioDto
                    {
                        IdPatio = 1,
                        NomePatio = "Pátio Central",
                        MotosTotaisPatio = 50,
                        MotosDisponiveisPatio = 35,
                        DataPatio = new DateTime(2024, 1, 15)
                    }
                },
                new CameraResponseDto
                {
                    IdCamera = 2,
                    NomeCamera = "Câmera Entrada Lateral",
                    IpCamera = "192.168.1.20",
                    Status = CameraStatus.Ativa,
                    PosX = 200.00f,
                    PosY = 150.00f,
                    Patio = new PatioDto
                    {
                        IdPatio = 2,
                        NomePatio = "Pátio Secundário",
                        MotosTotaisPatio = 30,
                        MotosDisponiveisPatio = 20,
                        DataPatio = new DateTime(2024, 2, 1)
                    }
                },
                new CameraResponseDto
                {
                    IdCamera = 3,
                    NomeCamera = "Câmera Saída",
                    IpCamera = "192.168.1.30",
                    Status = CameraStatus.Inativa,
                    PosX = 300.25f,
                    PosY = 120.75f,
                    Patio = new PatioDto
                    {
                        IdPatio = 3,
                        NomePatio = "Pátio Leste",
                        MotosTotaisPatio = 25,
                        MotosDisponiveisPatio = 15,
                        DataPatio = new DateTime(2024, 1, 20)
                    }
                },
                new CameraResponseDto
                {
                    IdCamera = 4,
                    NomeCamera = "Câmera Monitoramento",
                    IpCamera = "192.168.1.40",
                    Status = CameraStatus.Ativa,
                    PosX = 150.0f,
                    PosY = 80.0f,
                    Patio = new PatioDto
                    {
                        IdPatio = 1,
                        NomePatio = "Pátio Central",
                        MotosTotaisPatio = 50,
                        MotosDisponiveisPatio = 35,
                        DataPatio = new DateTime(2024, 1, 15)
                    }
                },
                new CameraResponseDto
                {
                    IdCamera = 5,
                    NomeCamera = "Câmera Estacionamento",
                    IpCamera = "192.168.1.50",
                    Status = CameraStatus.Ativa,
                    PosX = 75.5f,
                    PosY = 45.2f,
                    Patio = new PatioDto
                    {
                        IdPatio = 4,
                        NomePatio = "Pátio VIP",
                        MotosTotaisPatio = 15,
                        MotosDisponiveisPatio = 10,
                        DataPatio = new DateTime(2024, 2, 10)
                    }
                }
            };
        }
    }
}
