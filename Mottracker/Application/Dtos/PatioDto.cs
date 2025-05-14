using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class PatioDto
    {
        [Required(ErrorMessage = "O ID do pátio é obrigatório.")]
        public int IdPatio { get; set; }

        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do pátio deve ter no máximo 100 caracteres.")]
        public string NomePatio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O total de motos deve ser um valor positivo.")]
        public int MotosTotaisPatio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade de motos disponíveis deve ser um valor positivo.")]
        public int MotosDisponiveisPatio { get; set; }

        [Required(ErrorMessage = "A data do pátio é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data do pátio inválida.")]
        public DateTime DataPatio { get; set; }

        public int? LayoutPatioId { get; set; }

        public int? EnderecoPatioId { get; set; }

        // Lista de IDs das motos associadas (opcional)
        public List<int>? MotosPatioIds { get; set; }
    }
}