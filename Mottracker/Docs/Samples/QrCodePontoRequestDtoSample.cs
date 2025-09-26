using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.QrCodePonto;

namespace Mottracker.Docs.Samples
{
    public class QrCodePontoRequestDtoSample : IExamplesProvider<QrCodePontoRequestDto>
    {
        public QrCodePontoRequestDto GetExamples()
        {
            return new QrCodePontoRequestDto
            {
                IdQrCodePonto = 0,
                IdentificadorQrCode = "QR001",
                PosX = 100.5f,
                PosY = 200.3f,
                LayoutPatioId = 1
            };
        }
    }
}
