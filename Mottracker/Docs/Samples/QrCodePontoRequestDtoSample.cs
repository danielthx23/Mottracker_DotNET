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
                IdentificadorQrCodePonto = "QR001",
                PosXQrCodePonto = 100.5f,
                PosYQrCodePonto = 200.3f,
                LayoutPatioQrCodePontoId = 1
            };
        }
    }
}
