using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos
{
    public class QrCodePontoDto
    {
        [Required(ErrorMessage = "O ID do QR Code é obrigatório.")]
        public int IdQrCodePonto { get; set; }

        [Required(ErrorMessage = "O identificador do QR Code é obrigatório.")]
        [StringLength(100, ErrorMessage = "O identificador do QR Code deve ter no máximo 100 caracteres.")]
        public string IdentificadorQrCode { get; set; }

        [Required(ErrorMessage = "A posição X é obrigatória.")]
        public float PosX { get; set; }

        [Required(ErrorMessage = "A posição Y é obrigatória.")]
        public float PosY { get; set; }

        public int? LayoutPatioId { get; set; }
    }
}