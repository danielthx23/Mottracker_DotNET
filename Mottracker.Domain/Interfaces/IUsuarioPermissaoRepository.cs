using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IUsuarioPermissaoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<UsuarioPermissaoEntity?> ObterPorIdAsync(int usuarioId, int permissaoId);
        Task<UsuarioPermissaoEntity?> SalvarAsync(UsuarioPermissaoEntity entity);
        Task<UsuarioPermissaoEntity?> AtualizarAsync(UsuarioPermissaoEntity entity);
        Task<UsuarioPermissaoEntity?> DeletarAsync(int usuarioId, int permissaoId);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterPorUsuarioIdAsync(long usuarioId);
        Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterPorPermissaoIdAsync(long permissaoId);
    }
}