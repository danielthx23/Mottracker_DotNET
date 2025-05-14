using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class LayoutPatioDto
    {
        [Required(ErrorMessage = "O ID do layout do pátio é obrigatório.")]
        public int IdLayoutPatio { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data de criação inválida.")]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "O ID do pátio é obrigatório.")]
        public int PatioLayoutPatioId { get; set; }

        // Lista de IDs dos QR Codes vinculados (caso necessário para operações de criação/atualização)
        public List<int> QrCodesLayoutPatioIds { get; set; }
    }
}