using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IUsuarioPermissaoRepository
    {
        IEnumerable<UsuarioPermissaoEntity> ObterTodos();
        UsuarioPermissaoEntity? ObterPorId(int id);
        UsuarioPermissaoEntity? Salvar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Atualizar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Deletar(int id);
    }
}
