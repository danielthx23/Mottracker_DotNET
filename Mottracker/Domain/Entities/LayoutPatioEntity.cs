using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public virtual PatioEntity PatioLayoutPatio { get; set; }
        
        public virtual ICollection<QrCodePontoEntity> QrCodesLayoutPatio { get; set; }
    }   
}