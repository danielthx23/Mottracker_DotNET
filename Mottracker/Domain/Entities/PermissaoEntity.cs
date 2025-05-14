using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_PERMISSAO")]
    public class PermissaoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPermissao { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomePermissao { get; set; }

        public string Descricao { get; set; }
        
        public virtual UsuarioPermissaoEntity? UsuariosPermissoes { get; set; }
    }
}