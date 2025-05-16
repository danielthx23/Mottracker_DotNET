using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IUsuarioPermissaoRepository
    {
        IEnumerable<UsuarioPermissaoEntity> ObterTodos();
        UsuarioPermissaoEntity? ObterPorId(int usuarioId, int permissaoId);
        List<UsuarioPermissaoEntity>? ObterPorIds(List<(int usuarioId, int permissaoId)> chaves);
        UsuarioPermissaoEntity? Salvar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Atualizar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Deletar(int usuarioId, int permissaoId);
    }
}
