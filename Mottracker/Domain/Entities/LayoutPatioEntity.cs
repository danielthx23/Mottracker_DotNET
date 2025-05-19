using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mottracker.Domain.Entities
{
    [Table("MT_LAYOUT_PATIO")]
    public class LayoutPatioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLayoutPatio { get; set; }
        
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        [Required]
        [Precision(10, 2)]
        public decimal Largura { get; set; }
        
        [Required]
        [Precision(10, 2)]
        public decimal Comprimento { get; set; }
        
        [Precision(10, 2)]
        public decimal Altura { get; set; }
        
        public int? PatioLayoutPatioId { get; set; }
        
        public virtual PatioEntity? PatioLayoutPatio { get; set; }

        public virtual ICollection<QrCodePontoEntity>? QrCodesLayoutPatio { get; set; } = new List<QrCodePontoEntity>();

    }   
}