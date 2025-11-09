using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres.")]
        public string SenhaUsuario { get; set; }
    }
}