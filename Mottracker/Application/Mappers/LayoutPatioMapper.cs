using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class LayoutPatioMapper
    {
        public static LayoutPatioEntity ToLayoutPatioEntity(this LayoutPatioRequestDto obj)
        {
            return new LayoutPatioEntity
            {
                IdLayoutPatio = obj.IdLayoutPatio,
                Descricao = obj.Descricao,
                DataCriacao = obj.DataCriacao,
                Largura = obj.Largura,
                Comprimento = obj.Comprimento,
                Altura = obj.Altura,
                PatioLayoutPatioId = obj.PatioId
            };
        }

        public static LayoutPatioDto ToLayoutPatioDto(this LayoutPatioEntity obj)
        {
            return new LayoutPatioDto
            {
                IdLayoutPatio = obj.IdLayoutPatio,
                Descricao = obj.Descricao,
                DataCriacao = obj.DataCriacao,
                Largura = obj.Largura,
                Comprimento = obj.Comprimento,
                Altura = obj.Altura
            };
        }

        public static LayoutPatioResponseDto ToLayoutPatioResponseDto(this LayoutPatioEntity obj)
        {
            return new LayoutPatioResponseDto
            {
                IdLayoutPatio = obj.IdLayoutPatio,
                Descricao = obj.Descricao,
                DataCriacao = obj.DataCriacao,
                Largura = obj.Largura,
                Comprimento = obj.Comprimento,
                Altura = obj.Altura,
                PatioLayoutPatio = obj.PatioLayoutPatio?.ToPatioDto(),
                QrCodesLayoutPatio = obj.QrCodesLayoutPatio?.Select(q => q.ToQrCodePontoDto()).ToList()
            };
        }
    }
}
