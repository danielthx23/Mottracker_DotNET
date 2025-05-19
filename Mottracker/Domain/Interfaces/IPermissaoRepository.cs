using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IPermissaoRepository
    {
        IEnumerable<PermissaoEntity> ObterTodos();
        PermissaoEntity? ObterPorId(int id);
        PermissaoEntity? Salvar(PermissaoEntity entity);
        PermissaoEntity? Atualizar(PermissaoEntity entity);
        PermissaoEntity? Deletar(int id);
        IEnumerable<PermissaoEntity> ObterPorNomeContendo(string nomePermissao);
        IEnumerable<PermissaoEntity> ObterPorDescricaoContendo(string descricao);
    }
}