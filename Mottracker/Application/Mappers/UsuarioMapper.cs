using Mottracker.Application.Dtos.Usuario;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioEntity ToUsuarioEntity(this UsuarioRequestDto obj)
        {
            return new UsuarioEntity
            {
                IdUsuario = obj.IdUsuario,
                NomeUsuario = obj.NomeUsuario,
                CPFUsuario = obj.CPFUsuario,
                SenhaUsuario = obj.SenhaUsuario,
                CNHUsuario = obj.CNHUsuario,
                EmailUsuario = obj.EmailUsuario,
                TokenUsuario = obj.TokenUsuario,
                DataNascimentoUsuario = obj.DataNascimentoUsuario,
                CriadoEmUsuario = obj.CriadoEmUsuario
            };
        }

        public static UsuarioDto ToUsuarioDto(this UsuarioEntity obj)
        {
            return new UsuarioDto
            {
                IdUsuario = obj.IdUsuario,
                NomeUsuario = obj.NomeUsuario,
                CPFUsuario = obj.CPFUsuario,
                SenhaUsuario = obj.SenhaUsuario,
                CNHUsuario = obj.CNHUsuario,
                EmailUsuario = obj.EmailUsuario,
                TokenUsuario = obj.TokenUsuario,
                DataNascimentoUsuario = obj.DataNascimentoUsuario,
                CriadoEmUsuario = obj.CriadoEmUsuario
            };
        }

        public static UsuarioResponseDto ToUsuarioResponseDto(this UsuarioEntity obj)
        {
            return new UsuarioResponseDto
            {
                IdUsuario = obj.IdUsuario,
                NomeUsuario = obj.NomeUsuario,
                CPFUsuario = obj.CPFUsuario,
                SenhaUsuario = obj.SenhaUsuario,
                CNHUsuario = obj.CNHUsuario,
                EmailUsuario = obj.EmailUsuario,
                TokenUsuario = obj.TokenUsuario,
                DataNascimentoUsuario = obj.DataNascimentoUsuario,
                CriadoEmUsuario = obj.CriadoEmUsuario,
                ContratoUsuario = obj.ContratoUsuario?.ToContratoDto(),
                Telefones = obj.Telefones?.Select(t => t.ToTelefoneDto()).ToList(),
                UsuarioPermissoes = obj.UsuarioPermissoes?.Select(up => up.ToUsuarioPermissaoDto()).ToList()
            };
        }
    }
}
