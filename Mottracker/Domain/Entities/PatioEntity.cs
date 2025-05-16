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
        
        public virtual ICollection<MotoEntity>? MotosPatioAtual { get; set; } = new List<MotoEntity>();
        
        public virtual ICollection<CameraEntity>? CamerasPatio { get; set; }
        
        public virtual LayoutPatioEntity? LayoutPatio { get; set; }
        
        public virtual EnderecoEntity? EnderecoPatio { get; set; }

    }   
}