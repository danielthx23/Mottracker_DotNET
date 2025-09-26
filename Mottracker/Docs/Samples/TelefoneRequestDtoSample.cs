using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Telefone;

namespace Mottracker.Docs.Samples
{
    public class TelefoneRequestDtoSample : IExamplesProvider<TelefoneRequestDto>
    {
        public TelefoneRequestDto GetExamples()
        {
            return new TelefoneRequestDto
            {
                IdTelefone = 0,
                Numero = "(11) 99999-9999",
                Tipo = "Celular",
                UsuarioId = 1
            };
        }
    }
}
