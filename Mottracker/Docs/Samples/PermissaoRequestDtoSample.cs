using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Permissao;

namespace Mottracker.Docs.Samples
{
    public class PermissaoRequestDtoSample : IExamplesProvider<PermissaoRequestDto>
    {
        public PermissaoRequestDto GetExamples()
        {
            return new PermissaoRequestDto
            {
                IdPermissao = 0,
                NomePermissao = "Administrador",
                Descricao = "Acesso total ao sistema"
            };
        }
    }
}
