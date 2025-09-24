using Swashbuckle.AspNetCore.Filters;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Docs.Samples
{
    public class TelefoneResponseListSample : IExamplesProvider<IEnumerable<TelefoneResponseDto>>
    {
        public IEnumerable<TelefoneResponseDto> GetExamples()
        {
            return new List<TelefoneResponseDto>
            {
                new TelefoneResponseDto
                {
                    IdTelefone = 1,
                    Numero = "(11) 91234-5678",
                    Tipo = "Celular",
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
                    }
                },
                new TelefoneResponseDto
                {
                    IdTelefone = 2,
                    Numero = "(11) 98765-4321",
                    Tipo = "Celular",
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
                    }
                },
                new TelefoneResponseDto
                {
                    IdTelefone = 3,
                    Numero = "(11) 3456-7890",
                    Tipo = "Comercial",
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
                    }
                },
                new TelefoneResponseDto
                {
                    IdTelefone = 4,
                    Numero = "(11) 2345-6789",
                    Tipo = "Residencial",
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
                    }
                },
                new TelefoneResponseDto
                {
                    IdTelefone = 5,
                    Numero = "(11) 8765-4321",
                    Tipo = "Celular",
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
                    }
                }
            };
        }
    }
}
