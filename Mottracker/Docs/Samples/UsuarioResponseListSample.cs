using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Docs.Samples
{
    public class UsuarioResponseListSample : IExamplesProvider<IEnumerable<UsuarioResponseDto>>
    {
        public IEnumerable<UsuarioResponseDto> GetExamples()
        {
            return new List<UsuarioResponseDto>
            {
                new UsuarioResponseDto
                {
                    IdUsuario = 1,
                    NomeUsuario = "João Silva",
                    CPFUsuario = "123.456.789-00",
                    SenhaUsuario = "senha123",
                    CNHUsuario = "12345678901",
                    EmailUsuario = "joao@email.com",
                    TokenUsuario = "token123",
                    DataNascimentoUsuario = new DateTime(1990, 5, 15),
                    CriadoEmUsuario = new DateTime(2024, 1, 1),
                    ContratoUsuario = new ContratoDto
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
                    Telefones = new List<TelefoneDto>
                    {
                        new TelefoneDto
                        {
                            IdTelefone = 1,
                            Numero = "(11) 91234-5678",
                            Tipo = "Celular"
                        },
                        new TelefoneDto
                        {
                            IdTelefone = 2,
                            Numero = "(11) 3456-7890",
                            Tipo = "Comercial"
                        }
                    },
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 1,
                            IdPermissao = 1,
                            PapelUsuarioPermissao = "Admin Principal"
                        }
                    }
                },
                new UsuarioResponseDto
                {
                    IdUsuario = 2,
                    NomeUsuario = "Maria Santos",
                    CPFUsuario = "987.654.321-00",
                    SenhaUsuario = "senha456",
                    CNHUsuario = "98765432109",
                    EmailUsuario = "maria@email.com",
                    TokenUsuario = "token456",
                    DataNascimentoUsuario = new DateTime(1985, 8, 20),
                    CriadoEmUsuario = new DateTime(2024, 1, 15),
                    ContratoUsuario = new ContratoDto
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
                    Telefones = new List<TelefoneDto>
                    {
                        new TelefoneDto
                        {
                            IdTelefone = 3,
                            Numero = "(11) 98765-4321",
                            Tipo = "Celular"
                        }
                    },
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 2,
                            IdPermissao = 2,
                            PapelUsuarioPermissao = "Operador Senior"
                        }
                    }
                },
                new UsuarioResponseDto
                {
                    IdUsuario = 3,
                    NomeUsuario = "Pedro Oliveira",
                    CPFUsuario = "456.789.123-00",
                    SenhaUsuario = "senha789",
                    CNHUsuario = "45678912345",
                    EmailUsuario = "pedro@email.com",
                    TokenUsuario = "token789",
                    DataNascimentoUsuario = new DateTime(1992, 3, 10),
                    CriadoEmUsuario = new DateTime(2024, 2, 1),
                    ContratoUsuario = null,
                    Telefones = new List<TelefoneDto>
                    {
                        new TelefoneDto
                        {
                            IdTelefone = 4,
                            Numero = "(11) 2345-6789",
                            Tipo = "Residencial"
                        }
                    },
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 3,
                            IdPermissao = 3,
                            PapelUsuarioPermissao = "Cliente Regular"
                        }
                    }
                }
            };
        }
    }
}
