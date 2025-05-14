using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class UsuarioPermissaoDto
    {
        [Required(ErrorMessage = "O ID da permissão do usuário é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O papel é obrigatório.")]
        [StringLength(100, ErrorMessage = "O papel deve ter no máximo 100 caracteres.")]
        public string Papel { get; set; }

        // IDs relacionados, úteis para vinculação em API
        public List<int>? UsuariosIds { get; set; }

        public List<int>? PermissoesIds { get; set; }
    }
}