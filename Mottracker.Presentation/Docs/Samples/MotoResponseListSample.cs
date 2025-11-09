using Mottracker.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Domain.Enums;

namespace Mottracker.Docs.Samples
{
    public class MotoResponseListSample : IExamplesProvider<IEnumerable<MotoResponseDto>>
    {
        public IEnumerable<MotoResponseDto> GetExamples()
        {
            return new List<MotoResponseDto>
            {
                new MotoResponseDto
                {
                    IdMoto = 1,
                    PlacaMoto = "ABC-1234",
                    ModeloMoto = "Mottu Sport",
                    AnoMoto = 2023,
                    IdentificadorMoto = "MOT001",
                    QuilometragemMoto = 5000,
                    EstadoMoto = Estados.NoPatioCorreto,
                    CondicoesMoto = "Bom estado, sem avarias",
                    MotoPatioOrigemId = 1,
                    ContratoMoto = new ContratoDto
                    {
                        IdContrato = 1,
                        ClausulasContrato = "Contrato de locação padrão",
                        DataDeEntradaContrato = new DateTime(2024, 1, 15),
                        HorarioDeDevolucaoContrato = new DateTime(2024, 1, 15, 18, 0, 0),
                        DataDeExpiracaoContrato = new DateTime(2024, 2, 15),
                        RenovacaoAutomaticaContrato = 0,
                        DataUltimaRenovacaoContrato = null,
                        NumeroRenovacoesContrato = 0,
                        AtivoContrato = 1,
                        ValorToralContrato = 150.00m,
                        QuantidadeParcelas = 1
                    },
                    MotoPatioAtual = new PatioDto
                    {
                        IdPatio = 1,
                        NomePatio = "Pátio Central",
                        MotosTotaisPatio = 50,
                        MotosDisponiveisPatio = 35,
                        DataPatio = new DateTime(2024, 1, 15)
                    }
                },
                new MotoResponseDto
                {
                    IdMoto = 2,
                    PlacaMoto = "XYZ-5678",
                    ModeloMoto = "Mottu Pop",
                    AnoMoto = 2024,
                    IdentificadorMoto = "MOT002",
                    QuilometragemMoto = 2000,
                    EstadoMoto = Estados.Retirada,
                    CondicoesMoto = "Excelente estado, nova",
                    MotoPatioOrigemId = 2,
                    ContratoMoto = new ContratoDto
                    {
                        IdContrato = 2,
                        ClausulasContrato = "Contrato premium com seguro",
                        DataDeEntradaContrato = new DateTime(2024, 2, 1),
                        HorarioDeDevolucaoContrato = new DateTime(2024, 2, 1, 19, 30, 0),
                        DataDeExpiracaoContrato = new DateTime(2024, 3, 1),
                        RenovacaoAutomaticaContrato = 1,
                        DataUltimaRenovacaoContrato = new DateTime(2024, 2, 15),
                        NumeroRenovacoesContrato = 1,
                        AtivoContrato = 1,
                        ValorToralContrato = 200.00m,
                        QuantidadeParcelas = 2
                    },
                    MotoPatioAtual = new PatioDto
                    {
                        IdPatio = 2,
                        NomePatio = "Pátio Paulista",
                        MotosTotaisPatio = 30,
                        MotosDisponiveisPatio = 20,
                        DataPatio = new DateTime(2024, 2, 1)
                    }
                },
                new MotoResponseDto
                {
                    IdMoto = 3,
                    PlacaMoto = "DEF-9012",
                    ModeloMoto = "Mottu E",
                    AnoMoto = 2023,
                    IdentificadorMoto = "MOT003",
                    QuilometragemMoto = 8000,
                    EstadoMoto = Estados.NoPatioErrado,
                    CondicoesMoto = "QR Code danificado, necessita manutenção",
                    MotoPatioOrigemId = 1,
                    ContratoMoto = new ContratoDto
                    {
                        IdContrato = 3,
                        ClausulasContrato = "Contrato de longa duração",
                        DataDeEntradaContrato = new DateTime(2024, 1, 1),
                        HorarioDeDevolucaoContrato = new DateTime(2024, 1, 1, 17, 0, 0),
                        DataDeExpiracaoContrato = new DateTime(2024, 4, 1),
                        RenovacaoAutomaticaContrato = 1,
                        DataUltimaRenovacaoContrato = new DateTime(2024, 2, 1),
                        NumeroRenovacoesContrato = 2,
                        AtivoContrato = 1,
                        ValorToralContrato = 300.00m,
                        QuantidadeParcelas = 3
                    },
                    MotoPatioAtual = new PatioDto
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
