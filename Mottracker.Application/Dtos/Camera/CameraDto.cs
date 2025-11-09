using System.ComponentModel.DataAnnotations;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.Dtos.Camera
{
    public class CameraDto
    {
        [Required(ErrorMessage = "O ID do contrato é obrigatório.")]
        public int IdCamera { get; set; }
        
        [Required(ErrorMessage = "O nome da câmera é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da câmera deve ter no máximo 100 caracteres.")]
        public string NomeCamera { get; set; }

        [StringLength(255, ErrorMessage = "O IP da câmera deve ter no máximo 255 caracteres.")]
        public string IpCamera { get; set; }

        [Required(ErrorMessage = "O status da câmera é obrigatório.")]
        public CameraStatus Status { get; set; }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Posição X inválida.")]
        public float? PosX { get; set; }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Posição Y inválida.")]
        public float? PosY { get; set; }
    }
}