using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Docs.Samples
{
    public class PatioRequestDtoSample : IExamplesProvider<PatioRequestDto>
    {
        public PatioRequestDto GetExamples()
        {
            return new PatioRequestDto
            {
                IdPatio = 0,
                NomePatio = "Pátio Central",
                MotosTotaisPatio = 50,
                MotosDisponiveisPatio = 35,
                DataPatio = new DateTime(2024, 1, 15),
            };
        }
    }
}
