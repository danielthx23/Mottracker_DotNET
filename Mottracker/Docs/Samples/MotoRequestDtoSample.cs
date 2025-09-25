using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Domain.Enums;

namespace Mottracker.Docs.Samples
{
    public class MotoRequestDtoSample : IExamplesProvider<MotoRequestDto>
    {
        public MotoRequestDto GetExamples()
        {
            return new MotoRequestDto
            {
                IdMoto = 0,
                PlacaMoto = "ABC-1234",
                ModeloMoto = "Mottu Sport",
                AnoMoto = 2023,
                IdentificadorMoto = "MOT001",
                QuilometragemMoto = 5000,
                EstadoMoto = Estados.NoPatioCorreto,
                CondicoesMoto = "Bom estado, sem avarias",
                MotoPatioOrigemId = 1,
                ContratoMotoId = 1,
                MotoPatioAtualId = 1
            };
        }
    }
}
