using System.ComponentModel.DataAnnotations;

namespace Mottracker.Application.Dtos.Endereco
{
    public class EnderecoDto
    {
        [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
        public int IdEndereco { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(150, ErrorMessage = "O logradouro deve ter no máximo 150 caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20, ErrorMessage = "O número deve ter no máximo 20 caracteres.")]
        public string Numero { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, ErrorMessage = "O estado deve ter exatamente 2 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres.")]
        public string CEP { get; set; }

        [StringLength(100, ErrorMessage = "A referência deve ter no máximo 100 caracteres.")]
        public string Referencia { get; set; }
    }
}