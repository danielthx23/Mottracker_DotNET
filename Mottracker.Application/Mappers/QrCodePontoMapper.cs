using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class QrCodePontoMapper
    {
        public static QrCodePontoEntity ToQrCodePontoEntity(this QrCodePontoRequestDto obj)
        {
            return new QrCodePontoEntity
            {
                IdQrCodePonto = obj.IdQrCodePonto,
                IdentificadorQrCode = obj.IdentificadorQrCode,
                PosX = obj.PosX,
                PosY = obj.PosY,
                LayoutPatioId = obj.LayoutPatioId
            };
        }

        public static QrCodePontoDto ToQrCodePontoDto(this QrCodePontoEntity obj)
        {
            return new QrCodePontoDto
            {
                IdQrCodePonto = obj.IdQrCodePonto,
                IdentificadorQrCode = obj.IdentificadorQrCode,
                PosX = obj.PosX,
                PosY = obj.PosY
            };
        }

        public static QrCodePontoResponseDto ToQrCodePontoResponseDto(this QrCodePontoEntity obj)
        {
            return new QrCodePontoResponseDto
            {
                IdQrCodePonto = obj.IdQrCodePonto,
                IdentificadorQrCode = obj.IdentificadorQrCode,
                PosX = obj.PosX,
                PosY = obj.PosY,
                LayoutPatio = obj.LayoutPatio?.ToLayoutPatioDto()
            };
        }
    }
}
