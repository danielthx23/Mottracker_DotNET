using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.UsuarioPermissao;

namespace Mottracker.Application.Dtos.Usuario
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string CPFUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string CNHUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string TokenUsuario { get; set; }
        public DateTime DataNascimentoUsuario { get; set; }
        public DateTime CriadoEmUsuario { get; set; }
        public ContratoDto? ContratoUsuario { get; set; }
        public List<TelefoneDto>? Telefones { get; set; }
        public List<UsuarioPermissaoDto>? UsuarioPermissoes { get; set; }
    }
}