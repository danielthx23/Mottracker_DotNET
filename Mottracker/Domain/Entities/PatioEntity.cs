using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_PATIO")]
    public class PatioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPatio { get; set; }
        
        [Required]
        public string NomePatio { get; set; }
        
        public int MotosTotaisPatio { get; set; }

        public int MotosDisponiveisPatio { get; set; }
        
        public DateTime DataPatio { get; set; }
        
        public virtual ICollection<MotoEntity>? MotosPatio { get; set; }
        
        // TODO: Localização e Layout
    }   
}