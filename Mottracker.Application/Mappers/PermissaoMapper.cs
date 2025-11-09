using Mottracker.Application.Dtos.Permissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class PermissaoMapper
    {
        public static PermissaoEntity ToPermissaoEntity(this PermissaoRequestDto obj)
        {
            return new PermissaoEntity
            {
                IdPermissao = obj.IdPermissao,
                NomePermissao = obj.NomePermissao,
                Descricao = obj.Descricao
            };
        }

        public static PermissaoDto ToPermissaoDto(this PermissaoEntity obj)
        {
            return new PermissaoDto
            {
                IdPermissao = obj.IdPermissao,
                NomePermissao = obj.NomePermissao,
                Descricao = obj.Descricao
            };
        }

        public static PermissaoResponseDto ToPermissaoResponseDto(this PermissaoEntity obj)
        {
            return new PermissaoResponseDto
            {
                IdPermissao = obj.IdPermissao,
                NomePermissao = obj.NomePermissao,
                Descricao = obj.Descricao,
                UsuarioPermissoes = obj.UsuarioPermissoes?.Select(up => up.ToUsuarioPermissaoDto()).ToList()
            };
        }
    }
}
