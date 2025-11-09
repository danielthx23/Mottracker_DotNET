using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.UsuarioPermissao
{
    public class UsuarioPermissaoDto
    {
        [Required(ErrorMessage = "O ID da permissão do usuário é obrigatório.")]
        public int? IdUsuario { get; set; }
        
        [Required(ErrorMessage = "O ID da permissão do usuário é obrigatório.")]
        public int? IdPermissao { get; set; }

        [Required(ErrorMessage = "O papel é obrigatório.")]
        [StringLength(100, ErrorMessage = "O papel deve ter no máximo 100 caracteres.")]
        public string PapelUsuarioPermissao { get; set; }
    }
}