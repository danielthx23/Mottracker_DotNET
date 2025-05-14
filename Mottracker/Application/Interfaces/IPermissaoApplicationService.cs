using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IPermissaoApplicationService
    {
        IEnumerable<PermissaoEntity> ObterTodosPermissoes();
        PermissaoEntity? ObterPermissaoPorId(int id);
        PermissaoEntity? SalvarDadosPermissao(PermissaoEntity entity);
        PermissaoEntity? EditarDadosPermissao(int id, PermissaoEntity entity);
        PermissaoEntity? DeletarDadosPermissao(int id);
    }
}
