using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Docs.Samples
{
    public class LayoutPatioRequestDtoSample : IExamplesProvider<LayoutPatioRequestDto>
    {
        public LayoutPatioRequestDto GetExamples()
        {
            return new LayoutPatioRequestDto
            {
                IdLayoutPatio = 0,
                NomeLayoutPatio = "Layout Principal",
                DescricaoLayoutPatio = "Layout padrão do pátio central",
                DataCriacaoLayoutPatio = new DateTime(2024, 1, 15),
                PatioLayoutPatioId = 1
            };
        }
    }
}
