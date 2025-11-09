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
                CEP = "01234-567",
                Logradouro = "Rua das Flores",
                Numero = "123",
                Complemento = "Apto 45",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                PatioEnderecoId = 1
            };
        }
    }
}
