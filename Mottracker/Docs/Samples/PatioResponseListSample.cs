using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Dtos.Endereco;

namespace Mottracker.Docs.Samples
{
    public class PatioResponseListSample : IExamplesProvider<IEnumerable<PatioResponseDto>>
    {
        public IEnumerable<PatioResponseDto> GetExamples()
        {
            return new List<PatioResponseDto>
            {
                new PatioResponseDto
                {
                    IdPatio = 1,
                    NomePatio = "Pátio Central",
                    MotosTotaisPatio = 50,
                    MotosDisponiveisPatio = 35,
                    DataPatio = new DateTime(2024, 1, 15),
                    MotosPatioAtual = new List<MotoDto>
                    {
                        new MotoDto
                        {
                            IdMoto = 1,
                            PlacaMoto = "ABC-1234",
                            ModeloMoto = "Mottu Sport",
                            AnoMoto = 2023,
                            IdentificadorMoto = "MOT001",
                            QuilometragemMoto = 5000,
                            EstadoMoto = Domain.Enums.Estados.NoPatioCorreto,
                            CondicoesMoto = "Bom estado",
                            MotoPatioOrigemId = 1
                        },
                        new MotoDto
                        {
                            IdMoto = 2,
                            PlacaMoto = "XYZ-5678",
                            ModeloMoto = "Mottu Pop",
                            AnoMoto = 2024,
                            IdentificadorMoto = "MOT002",
                            QuilometragemMoto = 2000,
                            EstadoMoto = Domain.Enums.Estados.NoPatioCorreto,
                            CondicoesMoto = "Excelente estado",
                            MotoPatioOrigemId = 1
                        }
                    },
                    CamerasPatio = new List<CameraDto>
                    {
                        new CameraDto
                        {
                            IdCamera = 1,
                            NomeCamera = "Câmera Portão Principal",
                            IpCamera = "192.168.1.10",
                            Status = Domain.Enums.CameraStatus.Ativa,
                            PosX = 123.45f,
                            PosY = 67.89f
                        },
                        new CameraDto
                        {
                            IdCamera = 2,
                            NomeCamera = "Câmera Entrada",
                            IpCamera = "192.168.1.11",
                            Status = Domain.Enums.CameraStatus.Ativa,
                            PosX = 100.0f,
                            PosY = 50.0f
                        }
                    },
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 1,
                        Descricao = "Layout padrão para pátio central",
                        DataCriacao = new DateTime(2024, 1, 15),
                        Largura = 25.50m,
                        Comprimento = 40.00m,
                        Altura = 3.00m
                    },
                    EnderecoPatio = new EnderecoDto
                    {
                        IdEndereco = 1,
                        Logradouro = "Rua das Flores",
                        Numero = "123",
                        Complemento = "Apto 45",
                        Bairro = "Centro",
                        Cidade = "São Paulo",
                        Estado = "SP",
                        CEP = "01234-567",
                        Referencia = "Próximo ao metrô"
                    }
                },
                new PatioResponseDto
                {
                    IdPatio = 2,
                    NomePatio = "Pátio Paulista",
                    MotosTotaisPatio = 30,
                    MotosDisponiveisPatio = 20,
                    DataPatio = new DateTime(2024, 2, 1),
                    MotosPatioAtual = new List<MotoDto>
                    {
                        new MotoDto
                        {
                            IdMoto = 3,
                            PlacaMoto = "DEF-9012",
                            ModeloMoto = "Mottu E",
                            AnoMoto = 2023,
                            IdentificadorMoto = "MOT003",
                            QuilometragemMoto = 8000,
                            EstadoMoto = Domain.Enums.Estados.NoPatioCorreto,
                            CondicoesMoto = "Necessita manutenção",
                            MotoPatioOrigemId = 2
                        }
                    },
                    CamerasPatio = new List<CameraDto>
                    {
                        new CameraDto
                        {
                            IdCamera = 3,
                            NomeCamera = "Câmera Saída",
                            IpCamera = "192.168.1.12",
                            Status = Domain.Enums.CameraStatus.Inativa,
                            PosX = 200.0f,
                            PosY = 150.0f
                        }
                    },
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 2,
                        Descricao = "Layout compacto para pátio secundário",
                        DataCriacao = new DateTime(2024, 2, 1),
                        Largura = 15.00m,
                        Comprimento = 20.00m,
                        Altura = 2.50m
                    },
                    EnderecoPatio = new EnderecoDto
                    {
                        IdEndereco = 2,
                        Logradouro = "Avenida Paulista",
                        Numero = "1000",
                        Complemento = "Sala 200",
                        Bairro = "Bela Vista",
                        Cidade = "São Paulo",
                        Estado = "SP",
                        CEP = "01310-100",
                        Referencia = "Edifício comercial"
                    }
                }
            };
        }
    }
}
