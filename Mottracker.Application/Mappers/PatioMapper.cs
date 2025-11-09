using Mottracker.Application.Dtos.Patio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class PatioMapper
    {
        public static PatioEntity ToPatioEntity(this PatioRequestDto obj)
        {
            return new PatioEntity
            {
                IdPatio = obj.IdPatio,
                NomePatio = obj.NomePatio,
                MotosTotaisPatio = obj.MotosTotaisPatio,
                MotosDisponiveisPatio = obj.MotosDisponiveisPatio,
                DataPatio = obj.DataPatio
            };
        }

        public static PatioDto ToPatioDto(this PatioEntity obj)
        {
            return new PatioDto
            {
                IdPatio = obj.IdPatio,
                NomePatio = obj.NomePatio,
                MotosTotaisPatio = obj.MotosTotaisPatio,
                MotosDisponiveisPatio = obj.MotosDisponiveisPatio,
                DataPatio = obj.DataPatio
            };
        }

        public static PatioResponseDto ToPatioResponseDto(this PatioEntity obj)
        {
            return new PatioResponseDto
            {
                IdPatio = obj.IdPatio,
                NomePatio = obj.NomePatio,
                MotosTotaisPatio = obj.MotosTotaisPatio,
                MotosDisponiveisPatio = obj.MotosDisponiveisPatio,
                DataPatio = obj.DataPatio,
                MotosPatioAtual = obj.MotosPatioAtual?.Select(m => m.ToMotoDto()).ToList(),
                CamerasPatio = obj.CamerasPatio?.Select(c => c.ToCameraDto()).ToList(),
                LayoutPatio = obj.LayoutPatio?.ToLayoutPatioDto(),
                EnderecoPatio = obj.EnderecoPatio?.ToEnderecoDto()
            };
        }
    }
}
