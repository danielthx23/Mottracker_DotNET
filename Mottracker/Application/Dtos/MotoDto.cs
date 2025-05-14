using System.ComponentModel.DataAnnotations;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Dtos
{
    public class MotoDto
    {
        [Required(ErrorMessage = "O ID da moto é obrigatório.")]
        public int IdMoto { get; set; }

        [Required(ErrorMessage = "A placa da moto é obrigatória.")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
        public string PlacaMoto { get; set; }

        [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
        [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres.")]
        public string ModeloMoto { get; set; }

        [Required(ErrorMessage = "O ano da moto é obrigatório.")]
        [Range(2000, 2100, ErrorMessage = "O ano da moto deve estar entre 2000 e 2100.")]
        public int AnoMoto { get; set; }

        [StringLength(100, ErrorMessage = "O identificador da moto deve ter no máximo 100 caracteres.")]
        public string IdentificadorMoto { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem deve ser um valor positivo.")]
        public int QuilometragemMoto { get; set; }

        [Required(ErrorMessage = "O estado da moto é obrigatório.")]
        public Estados EstadoMoto { get; set; }

        [StringLength(500, ErrorMessage = "As condições da moto devem ter no máximo 500 caracteres.")]
        public string CondicoesMoto { get; set; }

        public int? ContratoMotoId { get; set; }

        public int? MotoPatioAtualId { get; set; }

        public int? MotoPatioOrigemId { get; set; }
    }
}