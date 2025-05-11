using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_USUARIO")]
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        
        [Required]
        public string NomeUsuario { get; set; }
        
        [Required]
        public string CPFUsuario { get; set; }
        
        public string EmailUsuario { get; set; }

        public string DataNascimentoUsuario { get; set; }
    }   
}