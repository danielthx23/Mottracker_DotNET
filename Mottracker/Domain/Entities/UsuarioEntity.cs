using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottracker.Domain.Entities
{
    [Table("MT_USUARIO")]
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        public string NomeUsuario { get; set; }

        [Required]
        public string CPFUsuario { get; set; }

        [Required]
        public string SenhaUsuario { get; set; }

        [Required]
        public string CNHUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string EmailUsuario { get; set; }

        public string TokenUsuario { get; set; }

        public DateTime DataNascimentoUsuario { get; set; }

        public DateTime CriadoEmUsuario { get; set; }
        
        public virtual ContratoEntity? ContratoUsuario { get; set; }

        public virtual ICollection<TelefoneEntity>? Telefones { get; set; } = new List<TelefoneEntity>();

        public virtual ICollection<UsuarioPermissaoEntity>? UsuarioPermissoes { get; set; } = new List<UsuarioPermissaoEntity>();
    }
}