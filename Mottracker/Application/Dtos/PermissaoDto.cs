using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class PermissaoDto
    {
        [Required(ErrorMessage = "O ID da permissão é obrigatório.")]
        public int IdPermissao { get; set; }

        [Required(ErrorMessage = "O nome da permissão é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da permissão deve ter no máximo 100 caracteres.")]
        public string NomePermissao { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        public int? UsuariosPermissoesId { get; set; }
    }
}