using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioEntity> ObterTodos();
        UsuarioEntity? ObterPorId(int id);
        List<UsuarioEntity>? ObterPorIds(List<int> id);
        UsuarioEntity? Salvar(UsuarioEntity entity);
        UsuarioEntity? Atualizar(UsuarioEntity entity);
        UsuarioEntity? Deletar(int id);
    }
}
