using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_USUARIO_PERMISSAO")]
    public class UsuarioPermissaoEntity
    {

        public string Papel { get; set; }
        
        public int? UsuarioId { get; set; }
        
        public virtual UsuarioEntity? Usuario { get; set; }

        public int? PermissaoId { get; set; }
        
        public virtual PermissaoEntity? Permissao { get; set; }
    }
}