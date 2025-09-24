using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class MotoMapper
    {
        public static MotoEntity ToMotoEntity(this MotoRequestDto obj)
        {
            return new MotoEntity
            {
                IdMoto = obj.IdMoto,
                PlacaMoto = obj.PlacaMoto,
                ModeloMoto = obj.ModeloMoto,
                AnoMoto = obj.AnoMoto,
                IdentificadorMoto = obj.IdentificadorMoto,
                QuilometragemMoto = obj.QuilometragemMoto,
                EstadoMoto = obj.EstadoMoto,
                CondicoesMoto = obj.CondicoesMoto,
                MotoPatioOrigemId = obj.MotoPatioOrigemId,
                ContratoMotoId = obj.ContratoMotoId,
                MotoPatioAtualId = obj.MotoPatioAtualId
            };
        }

        public static MotoDto ToMotoDto(this MotoEntity obj)
        {
            return new MotoDto
            {
                IdMoto = obj.IdMoto,
                PlacaMoto = obj.PlacaMoto,
                ModeloMoto = obj.ModeloMoto,
                AnoMoto = obj.AnoMoto,
                IdentificadorMoto = obj.IdentificadorMoto,
                QuilometragemMoto = obj.QuilometragemMoto,
                EstadoMoto = obj.EstadoMoto,
                CondicoesMoto = obj.CondicoesMoto,
                MotoPatioOrigemId = obj.MotoPatioOrigemId
            };
        }

        public static MotoResponseDto ToMotoResponseDto(this MotoEntity obj)
        {
            return new MotoResponseDto
            {
                IdMoto = obj.IdMoto,
                PlacaMoto = obj.PlacaMoto,
                ModeloMoto = obj.ModeloMoto,
                AnoMoto = obj.AnoMoto,
                IdentificadorMoto = obj.IdentificadorMoto,
                QuilometragemMoto = obj.QuilometragemMoto,
                EstadoMoto = obj.EstadoMoto,
                CondicoesMoto = obj.CondicoesMoto,
                MotoPatioOrigemId = obj.MotoPatioOrigemId,
                ContratoMoto = obj.ContratoMoto?.ToContratoDto(),
                MotoPatioAtual = obj.MotoPatioAtual?.ToPatioDto()
            };
        }
    }
}
