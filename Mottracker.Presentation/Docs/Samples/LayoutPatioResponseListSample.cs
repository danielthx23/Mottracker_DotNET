using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Dtos.QrCodePonto;

namespace Mottracker.Docs.Samples
{
    public class LayoutPatioResponseListSample : IExamplesProvider<IEnumerable<LayoutPatioResponseDto>>
    {
        public IEnumerable<LayoutPatioResponseDto> GetExamples()
        {
            return new List<LayoutPatioResponseDto>
            {
                new LayoutPatioResponseDto
                {
                    IdLayoutPatio = 1,
                    Descricao = "Layout padrão para pátio central com 50 vagas",
                    DataCriacao = new DateTime(2024, 1, 15),
                    Largura = 25.50m,
                    Comprimento = 40.00m,
                    Altura = 3.00m,
                    PatioLayoutPatio = new PatioDto
                    {
                        IdPatio = 1,
                        NomePatio = "Pátio Central",
                        MotosTotaisPatio = 50,
                        MotosDisponiveisPatio = 35,
                        DataPatio = new DateTime(2024, 1, 15)
                    },
                    QrCodesLayoutPatio = new List<QrCodePontoDto>
                    {
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 1,
                            IdentificadorQrCode = "QR001",
                            PosX = 10.5f,
                            PosY = 15.2f
                        },
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 2,
                            IdentificadorQrCode = "QR002",
                            PosX = 20.3f,
                            PosY = 25.8f
                        }
                    }
                },
                new LayoutPatioResponseDto
                {
                    IdLayoutPatio = 2,
                    Descricao = "Layout compacto para pátio secundário",
                    DataCriacao = new DateTime(2024, 2, 1),
                    Largura = 15.00m,
                    Comprimento = 20.00m,
                    Altura = 2.50m,
                    PatioLayoutPatio = new PatioDto
                    {
                        IdPatio = 2,
                        NomePatio = "Pátio Secundário",
                        MotosTotaisPatio = 30,
                        MotosDisponiveisPatio = 20,
                        DataPatio = new DateTime(2024, 2, 1)
                    },
                    QrCodesLayoutPatio = new List<QrCodePontoDto>
                    {
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 3,
                            IdentificadorQrCode = "QR003",
                            PosX = 5.0f,
                            PosY = 8.5f
                        }
                    }
                },
                new LayoutPatioResponseDto
                {
                    IdLayoutPatio = 3,
                    Descricao = "Layout premium para pátio VIP",
                    DataCriacao = new DateTime(2024, 1, 20),
                    Largura = 30.00m,
                    Comprimento = 50.00m,
                    Altura = 4.00m,
                    PatioLayoutPatio = new PatioDto
                    {
                        IdPatio = 3,
                        NomePatio = "Pátio VIP",
                        MotosTotaisPatio = 25,
                        MotosDisponiveisPatio = 15,
                        DataPatio = new DateTime(2024, 1, 20)
                    },
                    QrCodesLayoutPatio = new List<QrCodePontoDto>
                    {
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 4,
                            IdentificadorQrCode = "QR004",
                            PosX = 12.0f,
                            PosY = 18.0f
                        },
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 5,
                            IdentificadorQrCode = "QR005",
                            PosX = 25.0f,
                            PosY = 35.0f
                        },
                        new QrCodePontoDto
                        {
                            IdQrCodePonto = 6,
                            IdentificadorQrCode = "QR006",
                            PosX = 8.0f,
                            PosY = 42.0f
                        }
                    }
                }
            };
        }
    }
}
