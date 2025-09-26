using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IPermissaoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<PermissaoEntity?> ObterPorIdAsync(int id);
        Task<PermissaoEntity?> SalvarAsync(PermissaoEntity entity);
        Task<PermissaoEntity?> AtualizarAsync(PermissaoEntity entity);
        Task<PermissaoEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterPorNomeAsync(string nomePermissao);
        Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterPorDescricaoAsync(string descricao);
    }
}