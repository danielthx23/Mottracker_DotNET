using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.Telefone
{
    public class TelefoneDto
    {
        [Required(ErrorMessage = "O ID do telefone é obrigatório.")]
        public int IdTelefone { get; set; }

        [Required(ErrorMessage = "O número do telefone é obrigatório.")]
        [StringLength(15, ErrorMessage = "O número do telefone deve ter no máximo 15 caracteres.")]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "Número de telefone inválido. Ex: (11) 91234-5678")]
        public string Numero { get; set; }

        [StringLength(20, ErrorMessage = "O tipo do telefone deve ter no máximo 20 caracteres.")]
        public string? Tipo { get; set; }  // Celular, Comercial, Residencial...
    }
}