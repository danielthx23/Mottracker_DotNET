using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_CONTRATO")]
    public class ContratoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContrato { get; set; }
        
        public string ClausulasContrato { get; set; }
        
        [Required]
        public DateTime DataDeEntradaContrato { get; set; }
        
        [Required]
        public DateTime HorarioDeDevolucaoContrato { get; set; }
        
        [Required]
        public DateTime DataDeExpiracaoContrato { get; set; }
        
        [Required]
        public bool RenovacaoAutomaticaContrato { get; set; }

        public DateTime? DataUltimaRenovacaoContrato { get; set; }
        
        public int NumeroRenovacoesContrato { get; set; } 
        
        public bool AtivoContrato { get; set; }
        
        [Required]
        public double ValorToralContrato { get; set; }
        
        [Required]
        public int QuantidadeParcelas  { get; set; }
        
        public virtual UsuarioEntity? UsuarioContrato { get; set; }
    }
}