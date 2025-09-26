using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class UsuarioPermissaoMapper
    {
        public static UsuarioPermissaoEntity ToUsuarioPermissaoEntity(this UsuarioPermissaoRequestDto obj)
        {
            return new UsuarioPermissaoEntity
            {
                UsuarioId = obj.IdUsuario,
                PermissaoId = obj.IdPermissao,
                Papel = obj.PapelUsuarioPermissao
            };
        }

        public static UsuarioPermissaoDto ToUsuarioPermissaoDto(this UsuarioPermissaoEntity obj)
        {
            return new UsuarioPermissaoDto
            {
                IdUsuario = obj.UsuarioId,
                IdPermissao = obj.PermissaoId,
                PapelUsuarioPermissao = obj.Papel
            };
        }

        public static UsuarioPermissaoResponseDto ToUsuarioPermissaoResponseDto(this UsuarioPermissaoEntity obj)
        {
            return new UsuarioPermissaoResponseDto
            {
                IdUsuario = obj.UsuarioId,
                IdPermissao = obj.PermissaoId,
                Usuario = obj.Usuario?.ToUsuarioDto(),
                Permissao = obj.Permissao?.ToPermissaoDto()
            };
        }
    }
}
