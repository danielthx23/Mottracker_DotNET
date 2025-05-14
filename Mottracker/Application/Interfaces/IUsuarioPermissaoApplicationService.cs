using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IUsuarioPermissaoApplicationService
    {
        IEnumerable<UsuarioPermissaoEntity> ObterTodosUsuarioPermissoes();
        UsuarioPermissaoEntity? ObterUsuarioPermissaoPorId(int id);
        UsuarioPermissaoEntity? SalvarDadosUsuarioPermissao(UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? EditarDadosUsuarioPermissao(int id, UsuarioPermissaoEntity entity);
        UsuarioPermissaoEntity? DeletarDadosUsuarioPermissao(int id);
    }
}
