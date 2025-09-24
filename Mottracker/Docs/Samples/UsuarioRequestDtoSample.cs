using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Docs.Samples
{
    public class UsuarioRequestDtoSample : IExamplesProvider<UsuarioRequestDto>
    {
        public UsuarioRequestDto GetExamples()
        {
            return new UsuarioRequestDto
            {
                IdUsuario = 0,
                NomeUsuario = "João Silva",
                CPFUsuario = "123.456.789-00",
                SenhaUsuario = "senha123",
                CNHUsuario = "12345678901",
                EmailUsuario = "joao@email.com",
                TokenUsuario = "token123",
                DataNascimentoUsuario = new DateTime(1990, 5, 15),
                CriadoEmUsuario = new DateTime(2024, 1, 1)
            };
        }
    }
}
