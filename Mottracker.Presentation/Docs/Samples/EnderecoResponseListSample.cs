using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Docs.Samples
{
    public class EnderecoResponseListSample : IExamplesProvider<IEnumerable<EnderecoResponseDto>>
    {
        public IEnumerable<EnderecoResponseDto> GetExamples()
        {
            return new List<EnderecoResponseDto>
            {
                new EnderecoResponseDto
                {
                    IdEndereco = 1,
                    Logradouro = "Rua das Flores",
                    Numero = "123",
                    Complemento = "Apto 45",
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "01234-567",
                    Referencia = "Próximo ao metrô",
                    PatioEndereco = new PatioDto
                    {
                        IdPatio = 1,
                        NomePatio = "Pátio Central",
                        MotosTotaisPatio = 50,
                        MotosDisponiveisPatio = 35,
                        DataPatio = new DateTime(2024, 1, 15)
                    }
                },
                new EnderecoResponseDto
                {
                    IdEndereco = 2,
                    Logradouro = "Avenida Paulista",
                    Numero = "1000",
                    Complemento = "Sala 200",
                    Bairro = "Bela Vista",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "01310-100",
                    Referencia = "Edifício comercial",
                    PatioEndereco = new PatioDto
                    {
                        IdPatio = 2,
                        NomePatio = "Pátio Paulista",
                        MotosTotaisPatio = 30,
                        MotosDisponiveisPatio = 20,
                        DataPatio = new DateTime(2024, 2, 1)
                    }
                },
                new EnderecoResponseDto
                {
                    IdEndereco = 3,
                    Logradouro = "Rua da Consolação",
                    Numero = "456",
                    Complemento = "",
                    Bairro = "Consolação",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "01302-000",
                    Referencia = "Próximo ao shopping",
                    PatioEndereco = new PatioDto
                    {
                        IdPatio = 3,
                        NomePatio = "Pátio Consolação",
                        MotosTotaisPatio = 25,
                        MotosDisponiveisPatio = 15,
                        DataPatio = new DateTime(2024, 1, 20)
                    }
                }
            };
        }
    }
}
