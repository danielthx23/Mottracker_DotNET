using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Contrato;

namespace Mottracker.Docs.Samples
{
    public class ContratoRequestDtoSample : IExamplesProvider<ContratoRequestDto>
    {
        public ContratoRequestDto GetExamples()
        {
            return new ContratoRequestDto
            {
                IdContrato = 0,
                ClausulasContrato = "Contrato de locação de moto por período determinado",
                DataDeEntradaContrato = new DateTime(2024, 1, 15),
                HorarioDeDevolucaoContrato = new DateTime(2024, 1, 15, 18, 0, 0),
                DataDeExpiracaoContrato = new DateTime(2024, 2, 15),
                RenovacaoAutomaticaContrato = 0,
                DataUltimaRenovacaoContrato = null,
                NumeroRenovacoesContrato = 0,
                AtivoContrato = 1,
                ValorToralContrato = 150.00m,
                QuantidadeParcelas = 1,
                UsuarioContratoId = 1
            };
        }
    }
}
