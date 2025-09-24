using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Docs.Samples
{
    public class UsuarioPermissaoRequestDtoSample : IExamplesProvider<UsuarioPermissaoRequestDto>
    {
        public UsuarioPermissaoRequestDto GetExamples()
        {
            return new UsuarioPermissaoRequestDto
            {
                IdUsuario = 1,
                IdPermissao = 1
            };
        }
    }
}
