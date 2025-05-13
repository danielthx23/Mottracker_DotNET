using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Entities
{
    [Table("MT_MOTO")]
    public class MotoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMoto{ get; set; }
    
        [Required]
        public string PlacaMoto { get; set; }
    
        [Required]
        public string ModeloMoto { get; set; } // Mottu Sport, Pop, E
    
        [Required]
        public int AnoMoto { get; set; }
        
        public string IdentificadorMoto { get; set; }
    
        public int QuilometragemMoto { get; set; }
    
        public Estados EstadoMoto { get; set; } // Retirada, no pátio, no pátio errado ou não devolvida.
    
        public string CondicoesMoto { get; set; } // QR Code danificado, IoT danificado, estado extraido com o IoT da Moto da Mottu (Não sabemos como funciona)
    
        public virtual ContratoEntity? ContratoMoto { get; set; }
    
        public virtual PatioEntity? MotoPatioAtual { get; set; }
        
        public virtual PatioEntity? MotoPatioOrigem { get; set; }
    }   
}