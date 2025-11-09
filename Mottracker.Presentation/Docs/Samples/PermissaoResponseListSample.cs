using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Docs.Samples
{
    public class PermissaoResponseListSample : IExamplesProvider<IEnumerable<PermissaoResponseDto>>
    {
        public IEnumerable<PermissaoResponseDto> GetExamples()
        {
            return new List<PermissaoResponseDto>
            {
                new PermissaoResponseDto
                {
                    IdPermissao = 1,
                    NomePermissao = "Administrador",
                    Descricao = "Acesso total ao sistema, incluindo gerenciamento de usuários e configurações",
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 1,
                            IdPermissao = 1,
                            PapelUsuarioPermissao = "Admin Principal"
                        },
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 2,
                            IdPermissao = 1,
                            PapelUsuarioPermissao = "Admin Secundário"
                        }
                    }
                },
                new PermissaoResponseDto
                {
                    IdPermissao = 2,
                    NomePermissao = "Operador",
                    Descricao = "Acesso para operações do dia a dia, gerenciamento de motos e contratos",
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 3,
                            IdPermissao = 2,
                            PapelUsuarioPermissao = "Operador Senior"
                        },
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 4,
                            IdPermissao = 2,
                            PapelUsuarioPermissao = "Operador Junior"
                        },
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 5,
                            IdPermissao = 2,
                            PapelUsuarioPermissao = "Operador Estagiário"
                        }
                    }
                },
                new PermissaoResponseDto
                {
                    IdPermissao = 3,
                    NomePermissao = "Cliente",
                    Descricao = "Acesso básico para clientes, visualização de contratos e histórico",
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 6,
                            IdPermissao = 3,
                            PapelUsuarioPermissao = "Cliente Premium"
                        },
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 7,
                            IdPermissao = 3,
                            PapelUsuarioPermissao = "Cliente Regular"
                        }
                    }
                },
                new PermissaoResponseDto
                {
                    IdPermissao = 4,
                    NomePermissao = "Relatórios",
                    Descricao = "Acesso para geração e visualização de relatórios do sistema",
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 8,
                            IdPermissao = 4,
                            PapelUsuarioPermissao = "Analista de Dados"
                        }
                    }
                },
                new PermissaoResponseDto
                {
                    IdPermissao = 5,
                    NomePermissao = "Manutenção",
                    Descricao = "Acesso para gerenciamento de manutenção de motos e equipamentos",
                    UsuarioPermissoes = new List<UsuarioPermissaoDto>
                    {
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 9,
                            IdPermissao = 5,
                            PapelUsuarioPermissao = "Técnico Senior"
                        },
                        new UsuarioPermissaoDto
                        {
                            IdUsuario = 10,
                            IdPermissao = 5,
                            PapelUsuarioPermissao = "Técnico Junior"
                        }
                    }
                }
            };
        }
    }
}
