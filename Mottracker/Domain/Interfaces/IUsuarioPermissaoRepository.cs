using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IUsuarioPermissaoRepository
    {
        IEnumerable<UsuarioPermissaoEntity> ObterTodos();
        UsuarioPermissaoEntity? ObterPorId(int usuarioId, int permissaoId);
        UsuarioPermissaoEntity? Salvar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Atualizar(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? Deletar(int usuarioId, int permissaoId);
        IEnumerable<UsuarioPermissaoEntity> ObterPorIdUsuario(long usuarioId);
        IEnumerable<UsuarioPermissaoEntity> ObterPorIdPermissao(long permissaoId);
    }
}