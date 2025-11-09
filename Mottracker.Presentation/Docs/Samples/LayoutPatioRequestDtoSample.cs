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
                Descricao = "Layout padrão do pátio central",
                DataCriacao = new DateTime(2024, 1, 15),
                Largura = 25.50m,
                Comprimento = 40.00m,
                Altura = 3.00m,
                PatioLayoutPatioId = 1
            };
        }
    }
}
