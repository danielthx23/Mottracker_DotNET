using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Docs.Samples
{
    public class UsuarioLoginDtoSample : IExamplesProvider<UsuarioLoginDto>
    {
        public UsuarioLoginDto GetExamples()
        {
            return new UsuarioLoginDto
            {
                EmailUsuario = "usuario@exemplo.com",
                SenhaUsuario = "senha123"
            };
        }
    }
}