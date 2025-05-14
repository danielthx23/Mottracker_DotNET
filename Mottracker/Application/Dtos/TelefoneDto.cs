using System.ComponentModel.DataAnnotations;

namespace CP2.API.Application.Dtos
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo 100 caracteres.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CPF deve ter exatamente 14 caracteres. Ex: 000.000.000-00")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Ex: 000.000.000-00")]
        public string CPFUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres.")]
        public string SenhaUsuario { get; set; }

        [Required(ErrorMessage = "A CNH é obrigatória.")]
        [StringLength(20, ErrorMessage = "A CNH deve ter no máximo 20 caracteres.")]
        public string CNHUsuario { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "Telefone inválido. Ex: (11) 91234-5678")]
        public string TelefoneUsuario { get; set; }

        [StringLength(255, ErrorMessage = "O token deve ter no máximo 255 caracteres.")]
        public string TokenUsuario { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida.")]
        public DateTime DataNascimentoUsuario { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CriadoEmUsuario { get; set; }

        // Representa o relacionamento com permissões
        public int? UsuarioPermissaoId { get; set; }

        // Lista de contratos relacionados (opcional)
        public List<int>? ContratosUsuarioIds { get; set; }
    }
}
