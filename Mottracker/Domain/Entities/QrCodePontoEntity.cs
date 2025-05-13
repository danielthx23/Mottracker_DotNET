using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    public class QrCodePontoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdQrCodePonto { get; set; }
        
        public string IdentificadorQrCode { get; set; }  
        
        public float PosX { get; set; }
        
        public float PosY { get; set; }
    
        public virtual LayoutPatioEntity? LayoutPatio { get; set; }
    }
}
