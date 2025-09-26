using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Moto;

namespace Mottracker.Docs.Samples
{
    public class ContratoResponseListSample : IExamplesProvider<IEnumerable<ContratoResponseDto>>
    {
        public IEnumerable<ContratoResponseDto> GetExamples()
        {
            return new List<ContratoResponseDto>
            {
                new ContratoResponseDto
                {
                    IdContrato = 1,
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
                    UsuarioContrato = new UsuarioDto
                    {
                        IdUsuario = 1,
                        NomeUsuario = "João Silva",
                        CPFUsuario = "123.456.789-00",
                        SenhaUsuario = "senha123",
                        CNHUsuario = "12345678901",
                        EmailUsuario = "joao@email.com",
                        TokenUsuario = "token123",
                        DataNascimentoUsuario = new DateTime(1990, 5, 15),
                        CriadoEmUsuario = new DateTime(2024, 1, 1)
                    },
                    MotoContrato = new MotoDto
                    {
                        IdMoto = 1,
                        PlacaMoto = "ABC-1234",
                        ModeloMoto = "Mottu Sport",
                        AnoMoto = 2023,
                        IdentificadorMoto = "MOT001",
                        QuilometragemMoto = 5000,
                        EstadoMoto = Domain.Enums.Estados.NoPatioCorreto,
                        CondicoesMoto = "Bom estado",
                        MotoPatioOrigemId = 1
                    }
                },
                new ContratoResponseDto
                {
                    IdContrato = 2,
                    ClausulasContrato = "Contrato de locação com renovação automática",
                    DataDeEntradaContrato = new DateTime(2024, 2, 1),
                    HorarioDeDevolucaoContrato = new DateTime(2024, 2, 1, 19, 30, 0),
                    DataDeExpiracaoContrato = new DateTime(2024, 3, 1),
                    RenovacaoAutomaticaContrato = 1,
                    DataUltimaRenovacaoContrato = new DateTime(2024, 2, 15),
                    NumeroRenovacoesContrato = 1,
                    AtivoContrato = 1,
                    ValorToralContrato = 200.00m,
                    QuantidadeParcelas = 2,
                    UsuarioContrato = new UsuarioDto
                    {
                        IdUsuario = 2,
                        NomeUsuario = "Maria Santos",
                        CPFUsuario = "987.654.321-00",
                        SenhaUsuario = "senha456",
                        CNHUsuario = "98765432109",
                        EmailUsuario = "maria@email.com",
                        TokenUsuario = "token456",
                        DataNascimentoUsuario = new DateTime(1985, 8, 20),
                        CriadoEmUsuario = new DateTime(2024, 1, 15)
                    },
                    MotoContrato = new MotoDto
                    {
                        IdMoto = 2,
                        PlacaMoto = "XYZ-5678",
                        ModeloMoto = "Mottu Pop",
                        AnoMoto = 2024,
                        IdentificadorMoto = "MOT002",
                        QuilometragemMoto = 2000,
                        EstadoMoto = Domain.Enums.Estados.Retirada,
                        CondicoesMoto = "Excelente estado",
                        MotoPatioOrigemId = 2
                    }
                }
            };
        }
    }
}
