using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_QRCODE_PONTO")]
    public class QrCodePontoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdQrCodePonto { get; set; }
        
        [Required]
        public string IdentificadorQrCode { get; set; }  
        
        public float PosX { get; set; }
        
        public float PosY { get; set; }

        public int? LayoutPatioId { get; set; }
        
        public virtual LayoutPatioEntity? LayoutPatio { get; set; }
    }
}