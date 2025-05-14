using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class ContratoDto
    {
        [Required(ErrorMessage = "O ID do contrato é obrigatório.")]
        public int IdContrato { get; set; }

        [StringLength(2000, ErrorMessage = "As cláusulas do contrato devem ter no máximo 2000 caracteres.")]
        public string ClausulasContrato { get; set; }

        [Required(ErrorMessage = "A data de entrada do contrato é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data de entrada inválida.")]
        public DateTime DataDeEntradaContrato { get; set; }

        [Required(ErrorMessage = "O horário de devolução do contrato é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "Horário de devolução inválido.")]
        public DateTime HorarioDeDevolucaoContrato { get; set; }

        [Required(ErrorMessage = "A data de expiração do contrato é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data de expiração inválida.")]
        public DateTime DataDeExpiracaoContrato { get; set; }

        [Required(ErrorMessage = "A renovação automática do contrato é obrigatória.")]
        public bool RenovacaoAutomaticaContrato { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data da última renovação inválida.")]
        public DateTime? DataUltimaRenovacaoContrato { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de renovações deve ser positivo.")]
        public int NumeroRenovacoesContrato { get; set; }

        public bool AtivoContrato { get; set; }

        [Required(ErrorMessage = "O valor total do contrato é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser um valor positivo.")]
        public decimal ValorToralContrato { get; set; }

        [Required(ErrorMessage = "A quantidade de parcelas é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de parcelas deve ser ao menos 1.")]
        public int QuantidadeParcelas { get; set; }

        public int? UsuarioContratoId { get; set; }

        public int? MotoContratoId { get; set; }
    }
}
