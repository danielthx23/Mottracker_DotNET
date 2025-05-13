using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    public class EnderecoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEndereco { get; set; }

        [Required]
        [MaxLength(150)]
        public string Logradouro { get; set; }  // Rua, avenida, etc.

        [Required]
        [MaxLength(20)]
        public string Numero { get; set; }

        [MaxLength(100)]
        public string Complemento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }  // Ex: "SP", "RJ"

        [Required]
        [MaxLength(10)]
        public string CEP { get; set; }

        [MaxLength(100)]
        public string Referencia { get; set; }  // Ponto de referência (opcional)
        
        public virtual PatioEntity? PatioEndereco { get; set; }
    }
}