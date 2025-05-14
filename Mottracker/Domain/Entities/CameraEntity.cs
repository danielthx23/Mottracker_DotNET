using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Entities
{
    [Table("MT_CAMERA")]
    public class CameraEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCamera { get; set; }

        [Required]
        public string NomeCamera { get; set; }

        [MaxLength(255)]
        public string IpCamera { get; set; }  

        [Required]
        public CameraStatus Status { get; set; }
        
        public float? PosX { get; set; }
        
        public float? PosY { get; set; }
        
        public virtual PatioEntity? Patio { get; set; }
    }
}