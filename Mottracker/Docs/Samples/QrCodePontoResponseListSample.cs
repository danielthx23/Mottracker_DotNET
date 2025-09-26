using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Docs.Samples
{
    public class QrCodePontoResponseListSample : IExamplesProvider<IEnumerable<QrCodePontoResponseDto>>
    {
        public IEnumerable<QrCodePontoResponseDto> GetExamples()
        {
            return new List<QrCodePontoResponseDto>
            {
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 1,
                    IdentificadorQrCode = "QR001",
                    PosX = 10.5f,
                    PosY = 15.2f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 1,
                        Descricao = "Layout padrão para pátio central",
                        DataCriacao = new DateTime(2024, 1, 15),
                        Largura = 25.50m,
                        Comprimento = 40.00m,
                        Altura = 3.00m
                    }
                },
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 2,
                    IdentificadorQrCode = "QR002",
                    PosX = 20.3f,
                    PosY = 25.8f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 1,
                        Descricao = "Layout padrão para pátio central",
                        DataCriacao = new DateTime(2024, 1, 15),
                        Largura = 25.50m,
                        Comprimento = 40.00m,
                        Altura = 3.00m
                    }
                },
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 3,
                    IdentificadorQrCode = "QR003",
                    PosX = 5.0f,
                    PosY = 8.5f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 2,
                        Descricao = "Layout compacto para pátio secundário",
                        DataCriacao = new DateTime(2024, 2, 1),
                        Largura = 15.00m,
                        Comprimento = 20.00m,
                        Altura = 2.50m
                    }
                },
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 4,
                    IdentificadorQrCode = "QR004",
                    PosX = 12.0f,
                    PosY = 18.0f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 3,
                        Descricao = "Layout premium para pátio VIP",
                        DataCriacao = new DateTime(2024, 1, 20),
                        Largura = 30.00m,
                        Comprimento = 50.00m,
                        Altura = 4.00m
                    }
                },
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 5,
                    IdentificadorQrCode = "QR005",
                    PosX = 25.0f,
                    PosY = 35.0f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 3,
                        Descricao = "Layout premium para pátio VIP",
                        DataCriacao = new DateTime(2024, 1, 20),
                        Largura = 30.00m,
                        Comprimento = 50.00m,
                        Altura = 4.00m
                    }
                },
                new QrCodePontoResponseDto
                {
                    IdQrCodePonto = 6,
                    IdentificadorQrCode = "QR006",
                    PosX = 8.0f,
                    PosY = 42.0f,
                    LayoutPatio = new LayoutPatioDto
                    {
                        IdLayoutPatio = 3,
                        Descricao = "Layout premium para pátio VIP",
                        DataCriacao = new DateTime(2024, 1, 20),
                        Largura = 30.00m,
                        Comprimento = 50.00m,
                        Altura = 4.00m
                    }
                }
            };
        }
    }
}
