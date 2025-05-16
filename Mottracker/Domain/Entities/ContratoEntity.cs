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
        
        [Column(TypeName = "NUMBER(1)")]
        public int RenovacaoAutomaticaContrato { get; set; }

        public DateTime? DataUltimaRenovacaoContrato { get; set; }
        
        public int NumeroRenovacoesContrato { get; set; } 
        
        [Column(TypeName = "NUMBER(1)")]
        public int AtivoContrato { get; set; }
        
        [Required]
        public decimal ValorToralContrato { get; set; }
        
        [Required]
        public int QuantidadeParcelas  { get; set; }
        
        public int? UsuarioContratoId { get; set; }
        
        public virtual UsuarioEntity? UsuarioContrato { get; set; }
        
        public virtual MotoEntity? MotoContrato { get; set; }
        
        [NotMapped]
        public bool IsRenovacaoAutomatica
        {
            get => RenovacaoAutomaticaContrato == 1;
            set => RenovacaoAutomaticaContrato = value ? 1 : 0;
        }
    }
}