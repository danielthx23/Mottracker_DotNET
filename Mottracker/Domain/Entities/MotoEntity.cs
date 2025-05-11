using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities;

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
    
    public int QuilometragemMoto { get; set; }
    
    public string EstadoMoto { get; set; } // Retirada, no pátio, no pátio errado ou não devolvida.
    
    public string CondicoesMoto { get; set; } // QR Code danificado, IoT danificado, estado extraido com o IoT da Moto da Mottu (Não sabemos como funciona)
    
    public virtual ContratoEntity? ContratoMoto { get; set; }
    
    // TODO: Relacionamento com pátio e pátio atual.
}