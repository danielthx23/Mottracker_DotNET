using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Endereco;

namespace Mottracker.Docs.Samples
{
    public class EnderecoRequestDtoSample : IExamplesProvider<EnderecoRequestDto>
    {
        public EnderecoRequestDto GetExamples()
        {
            return new EnderecoRequestDto
            {
                IdEndereco = 0,
                CepEndereco = "01234-567",
                LogradouroEndereco = "Rua das Flores",
                NumeroEndereco = "123",
                ComplementoEndereco = "Apto 45",
                BairroEndereco = "Centro",
                CidadeEndereco = "São Paulo",
                EstadoEndereco = "SP",
                PatioEnderecoId = 1
            };
        }
    }
}
