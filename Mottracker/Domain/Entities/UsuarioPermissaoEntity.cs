using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_USUARIO_PERMISSAO")]
    public class UsuarioPermissaoEntity
    {
        [Key]
        public int Id { get; set; }

        public string Papel { get; set; }
        
        public virtual ICollection<UsuarioEntity>? Usuarios { get; set; }
        
        public virtual ICollection<PermissaoEntity>? Permissoes { get; set; }
    }
}