using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_TELEFONE")]
    public class TelefoneEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTelefone { get; set; }

        [Required]
        public string Numero { get; set; }  // Ex: "(11) 91234-5678"
        
        public string Tipo { get; set; }  // Ex: "Celular", "Comercial", "Residencial", etc.
        
        public int? UsuarioId { get; set; }
        
        public virtual UsuarioEntity? Usuario { get; set; }
    }
}