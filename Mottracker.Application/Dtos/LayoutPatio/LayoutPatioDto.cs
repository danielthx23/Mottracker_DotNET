using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.LayoutPatio
{
    public class LayoutPatioDto
    {
        [Required(ErrorMessage = "O ID do layout do pátio é obrigatório.")]
        public int IdLayoutPatio { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data de criação inválida.")]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero.")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero.")]
        public decimal Comprimento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A altura deve ser zero ou maior.")]
        public decimal Altura { get; set; }
    }
}