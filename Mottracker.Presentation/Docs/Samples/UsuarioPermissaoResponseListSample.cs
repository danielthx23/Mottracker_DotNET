using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Permissao;

namespace Mottracker.Docs.Samples
{
    public class UsuarioPermissaoResponseListSample : IExamplesProvider<IEnumerable<UsuarioPermissaoResponseDto>>
    {
        public IEnumerable<UsuarioPermissaoResponseDto> GetExamples()
        {
            return new List<UsuarioPermissaoResponseDto>
            {
                new UsuarioPermissaoResponseDto
                {
                    IdUsuario = 1,
                    IdPermissao = 1,
                    PapelUsuarioPermissao = "Admin Principal",
                    Usuario = new UsuarioDto
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
                    Permissao = new PermissaoDto
                    {
                        IdPermissao = 1,
                        NomePermissao = "Administrador",
                        Descricao = "Acesso total ao sistema, incluindo gerenciamento de usuários e configurações"
                    }
                },
                new UsuarioPermissaoResponseDto
                {
                    IdUsuario = 2,
                    IdPermissao = 2,
                    PapelUsuarioPermissao = "Operador Senior",
                    Usuario = new UsuarioDto
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
                    Permissao = new PermissaoDto
                    {
                        IdPermissao = 2,
                        NomePermissao = "Operador",
                        Descricao = "Acesso para operações do dia a dia, gerenciamento de motos e contratos"
                    }
                },
                new UsuarioPermissaoResponseDto
                {
                    IdUsuario = 3,
                    IdPermissao = 3,
                    PapelUsuarioPermissao = "Cliente Premium",
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = 3,
                        NomeUsuario = "Pedro Oliveira",
                        CPFUsuario = "456.789.123-00",
                        SenhaUsuario = "senha789",
                        CNHUsuario = "45678912345",
                        EmailUsuario = "pedro@email.com",
                        TokenUsuario = "token789",
                        DataNascimentoUsuario = new DateTime(1992, 3, 10),
                        CriadoEmUsuario = new DateTime(2024, 2, 1)
                    },
                    Permissao = new PermissaoDto
                    {
                        IdPermissao = 3,
                        NomePermissao = "Cliente",
                        Descricao = "Acesso básico para clientes, visualização de contratos e histórico"
                    }
                },
                new UsuarioPermissaoResponseDto
                {
                    IdUsuario = 4,
                    IdPermissao = 4,
                    PapelUsuarioPermissao = "Analista de Dados",
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = 4,
                        NomeUsuario = "Ana Costa",
                        CPFUsuario = "789.123.456-00",
                        SenhaUsuario = "senha012",
                        CNHUsuario = "78912345678",
                        EmailUsuario = "ana@email.com",
                        TokenUsuario = "token012",
                        DataNascimentoUsuario = new DateTime(1988, 12, 5),
                        CriadoEmUsuario = new DateTime(2024, 1, 20)
                    },
                    Permissao = new PermissaoDto
                    {
                        IdPermissao = 4,
                        NomePermissao = "Relatórios",
                        Descricao = "Acesso para geração e visualização de relatórios do sistema"
                    }
                },
                new UsuarioPermissaoResponseDto
                {
                    IdUsuario = 5,
                    IdPermissao = 5,
                    PapelUsuarioPermissao = "Técnico Senior",
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = 5,
                        NomeUsuario = "Carlos Ferreira",
                        CPFUsuario = "321.654.987-00",
                        SenhaUsuario = "senha345",
                        CNHUsuario = "32165498765",
                        EmailUsuario = "carlos@email.com",
                        TokenUsuario = "token345",
                        DataNascimentoUsuario = new DateTime(1995, 7, 25),
                        CriadoEmUsuario = new DateTime(2024, 2, 10)
                    },
                    Permissao = new PermissaoDto
                    {
                        IdPermissao = 5,
                        NomePermissao = "Manutenção",
                        Descricao = "Acesso para gerenciamento de manutenção de motos e equipamentos"
                    }
                }
            };
        }
    }
}
